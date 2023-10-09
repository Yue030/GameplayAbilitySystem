using Memezuki.GameplayAbilitySystem.Tag;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Memezuki.GameplayAbilitySystem.Attribute
{
    /// <summary>
    /// 屬性
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay Ability System/Attribute")]
    public class Attribute : ScriptableObject
    {
        /// <summary>
        /// 屬性值
        /// </summary>
        public AttributeValue AttributeValue;

        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// 判斷是否相同屬性的標籤
        /// </summary>
        public int Tag;

        private void OnValidate()
        {
            this.Tag = this.GetInstanceID();
        }

        /// <summary>
        /// 計算屬性初始值
        /// </summary>
        /// <remarks>由於這裡回傳的是 <see cref="struct"/> 所以更改後不會影響到原本的值</remarks>
        /// <param name="otherAttribute">其他屬性</param>
        /// <returns>屬性初始值</returns>
        public virtual AttributeValue CalculateInitialValue(List<Attribute> otherAttribute)
        {
            return this.AttributeValue;
        }

        /// <summary>
        /// 屬性經過修飾符後計算的值
        /// </summary>
        /// <remarks>由於這裡回傳的是 <see cref="struct"/> 所以更改後不會影響到原本的值</remarks>
        /// <param name="otherAttribute">其他屬性</param>
        /// <returns>屬性經過修飾符後計算的值</returns>
        public virtual AttributeValue CalculateAttributeValue(List<Attribute> otherAttribute)
        {
            AttributeValue newValue = this.AttributeValue;
            newValue.CurrentValue = (this.AttributeValue.BaseValue + this.AttributeValue.Modifier.Add) * (this.AttributeValue.Modifier.Multiply + 1);

            if (this.AttributeValue.Modifier.Override.HasValue)
            {
                newValue.CurrentValue = this.AttributeValue.Modifier.Override.Value;
            }

            return newValue;
        }

        /// <summary>
        /// 比較 <paramref name="obj"/> 與此屬性是否相同
        /// </summary>
        /// <param name="obj">比較的物件</param>
        /// <returns><paramref name="obj"/> 與此屬性是否相同</returns>
        public override bool Equals(object obj)
        {
            return obj is Attribute && this == (Attribute)obj;
        }

        /// <summary>
        /// 取得此屬性的 HashCode
        /// </summary>
        /// <returns>此屬性的 HashCode</returns>
        public override int GetHashCode()
        {
            return this.Tag.GetHashCode();
        }

        /// <summary>
        /// 比較兩個屬性是否相同
        /// </summary>
        /// <param name="x">第一個屬性</param>
        /// <param name="y">第二個屬性</param>
        /// <returns>標籤是否相同</returns>
        public static bool operator ==(Attribute x, Attribute y)
        {
            return x.Tag == y.Tag;
        }

        /// <summary>
        /// 比較兩個屬性是否不一樣
        /// </summary>
        /// <param name="x">第一個屬性</param>
        /// <param name="y">第二個屬性</param>
        /// <returns>標籤是否不一樣</returns>
        public static bool operator !=(Attribute x, Attribute y)
        {
            return !(x == y);
        }
    }
}
