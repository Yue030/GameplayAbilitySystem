using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
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

        private void Awake()
        {
            this.InitializeAttributes();
        }

        private void LateUpdate()
        {
            this.UpdateAttributeCurrentValue();
        }

        /// <summary>
        /// 設定屬性的基底值
        /// </summary>
        /// <param name="attribute">屬性</param>
        /// <param name="value">基底值</param>
        public void SetAttributeBaseValue(Attribute attribute, float value)
        {
            AttributeValue newValue = attribute.AttributeValue;
            newValue.BaseValue = value;
            for (int i = 0; i < this._attributeEventHandlers.Length; i++)
            {
                this._attributeEventHandlers[i].PreAttributeBaseChange(this, attribute, ref newValue);
            }
            attribute.AttributeValue = newValue;
        }

        /// <summary>
        /// 更新屬性的修飾符
        /// </summary>
        /// <param name="attribute">屬性</param>
        /// <param name="modifier">修飾符</param>
        public void UpdateAttributeModifiers(Attribute attribute, AttributeModifier modifier)
        {
            attribute.AttributeValue.Modifier = attribute.AttributeValue.Modifier.Combine(modifier);
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
            AttributeValue newValue = attribute.CalculateAttributeValue(this._attributes);
            for (int i = 0; i < this._attributeEventHandlers.Length; i++)
            {
                this._attributeEventHandlers[i].PreAttributeChange(this, attribute, ref newValue);
            }
            attribute.AttributeValue = newValue;
        }

        /// <summary>
        /// 新增屬性
        /// </summary>
        /// <param name="attributes">屬性</param>
        public void AddAttributes(params Attribute[] attributes)
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                if (this._attributes.Contains(attributes[i]))
                {
                    continue;
                }

                this._attributes.Add(attributes[i]);
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
        /// 取得現在遊戲角色的屬性
        /// </summary>
        public ReadOnlyCollection<Attribute> ReadOnlyAttributes
        {
            get
            {
                return new ReadOnlyCollection<Attribute>(this._attributes);
            }
        }
    }
}
