using Codice.Client.BaseCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;

namespace Memezuki.GameplayAbilitySystem.Attribute
{
    /// <summary>
    /// 管理屬性的系統
    /// </summary>
    public class AttributeSystem : MonoBehaviour
    {
        /// <summary>
        /// 屬性事件處理器集合
        /// </summary>
        [SerializeField]
        private AttributeEventHandlerBase[] _attributeEventHandlers;

        /// <summary>
        /// 給予現在遊戲角色的屬性
        /// </summary>
        [SerializeField]
        private List<Attribute> _attributes;

        /// <summary>
        /// 屬性索引快取
        /// </summary>
        private Dictionary<Attribute, int> _attributeIndexCache = new Dictionary<Attribute, int>();

        /// <summary>
        /// 快取是否失效
        /// </summary>
        private bool _isCacheInvalid;

        private void Awake()
        {
            this.InitializeAttributes();
            this.InvalidateAttributeCache();
        }

        private void LateUpdate()
        {
            this.UpdateAttributeCurrentValue();
        }

        /// <summary>
        /// 取得屬性值
        /// </summary>
        /// <remarks>由於這裡回傳的是 <see cref="struct"/> 所以更改後不會影響到原本的值</remarks>
        /// <param name="attribute">要取得的屬性</param>
        /// <param name="value">回傳的屬性值</param>
        /// <returns>是否有成功取到屬性值</returns>
        public bool GetAttributeValue(Attribute attribute, out AttributeValue value)
        {
            if (this.AttributeIndexCache.TryGetValue(attribute, out int index))
            {
                value = this._attributes[index].AttributeValue;
                return true;
            }

            value = new AttributeValue();
            Debug.LogWarning($"嘗試從快取取得屬性 {attribute.DisplayName}({attribute.Tag}) 的索引，但未成功。");
            return false;
        }

        /// <summary>
        /// 設定屬性的基底值
        /// </summary>
        /// <param name="attribute">屬性</param>
        /// <param name="value">基底值</param>
        public void SetAttributeBaseValue(Attribute attribute, float value)
        {
            if (!this.AttributeIndexCache.TryGetValue(attribute, out int index))
            {
                Debug.LogWarning($"嘗試從快取取得屬性 {attribute.DisplayName}({attribute.Tag}) 的索引，但未成功。");
                return;
            }

            Attribute targetAttribute = this._attributes[index];
            AttributeValue newValue = targetAttribute.AttributeValue;
            newValue.BaseValue = value;
            for (int i = 0; i < this._attributeEventHandlers.Length; i++)
            {
                this._attributeEventHandlers[i].PreAttributeBaseChange(this, attribute, ref newValue);
            }
            targetAttribute.AttributeValue = newValue;
        }

        /// <summary>
        /// 更新屬性的修飾符
        /// </summary>
        /// <param name="attribute">屬性</param>
        /// <param name="modifier">修飾符</param>
        public void UpdateAttributeModifiers(Attribute attribute, AttributeModifier modifier)
        {
            if (!this.AttributeIndexCache.TryGetValue(attribute, out int index))
            {
                Debug.LogWarning($"嘗試從快取取得屬性 {attribute.DisplayName}({attribute.Tag}) 的索引，但未成功。");
                return;
            }

            Attribute targetAttribute = this._attributes[index];
            AttributeValue newValue = targetAttribute.AttributeValue;
            newValue.Modifier = newValue.Modifier.Combine(modifier);
            targetAttribute.AttributeValue = newValue;
        }

        /// <summary>
        /// 更新所有屬性的當前值
        /// </summary>
        public void UpdateAttributeCurrentValue()
        {
            for (int i = 0; i < this._attributes.Count; i++)
            {
                this.UpdateAttributeCurrentValue(this._attributes[i]);
            }
        }

        /// <summary>
        /// 更新特定屬性的當前值
        /// </summary>
        /// <param name="attribute">屬性</param>
        public void UpdateAttributeCurrentValue(Attribute attribute)
        {
            if (!this.AttributeIndexCache.TryGetValue(attribute, out int index))
            {
                Debug.LogWarning($"嘗試從快取取得屬性 {attribute.DisplayName}({attribute.Tag}) 的索引，但未成功。");
                return;
            }

            Attribute targetAttribute = this._attributes[index];
            AttributeValue newValue = targetAttribute.CalculateAttributeValue(this._attributes);
            for (int i = 0; i < this._attributeEventHandlers.Length; i++)
            {
                this._attributeEventHandlers[i].PreAttributeChange(this, attribute, ref newValue);
            }

            targetAttribute.AttributeValue = newValue;
        }

        /// <summary>
        /// 新增屬性
        /// </summary>
        /// <param name="attributes">屬性</param>
        public void AddAttributes(params Attribute[] attributes)
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                if (this.AttributeIndexCache.ContainsKey(attributes[i]))
                {
                    continue;
                }

                this._attributes.Add(attributes[i]);
                this.AttributeIndexCache.Add(attributes[i], this._attributes.Count - 1);
            }
        }

        /// <summary>
        /// 刪除屬性
        /// </summary>
        /// <param name="attributes">屬性</param>
        public void RemoveAttributes(params Attribute[] attributes)
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                this._attributes.Remove(attributes[i]);
            }

            this.InvalidateAttributeCache();
        }

        /// <summary>
        /// 初始化屬性
        /// </summary>
        public void InitializeAttributes()
        {
            for (int i = 0; i < this._attributes.Count; i++)
            {
                this._attributes[i].AttributeValue = new AttributeValue();
            }
        }

        /// <summary>
        /// 重置所有屬性的修飾符
        /// </summary>
        public void ResetAllAttributeModifiers()
        {
            for (int i = 0; i < this._attributes.Count; i++)
            {
                this._attributes[i].AttributeValue.Modifier = default;
            }
        }

        /// <summary>
        /// 將屬性索引快取失效
        /// </summary>
        public void InvalidateAttributeCache()
        {
            this._isCacheInvalid = true;
        }

        /// <summary>
        /// 取得或設定屬性索引快取
        /// </summary>
        private Dictionary<Attribute, int> AttributeIndexCache
        {
            get
            {
                if (this._isCacheInvalid)
                {
                    this._attributeIndexCache.Clear();
                    for (int i = 0; i < this._attributes.Count; i++)
                    {
                        this._attributeIndexCache.Add(this._attributes[i], i);
                    }
                    this._isCacheInvalid = false;
                }

                return this._attributeIndexCache;
            }
            set
            {
                this._attributeIndexCache = value;
            }
        }

        /// <summary>
        /// 取得屬性索引快取
        /// </summary>
        public ReadOnlyDictionary<Attribute, int> ReadOnlyAttributeIndexCache
        {
            get
            {
                return new ReadOnlyDictionary<Attribute, int>(this._attributeIndexCache);
            }
        }
    }
}
