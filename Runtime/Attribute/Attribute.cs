using System;
using System.Collections.Generic;
using UnityEngine;

namespace Memezuki.GameplayAbilitySystem.Attribute
{
    /// <summary>
    /// 屬性
    /// </summary>
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
        /// 計算屬性初始值
        /// </summary>
        /// <param name="otherAttribute">其他屬性</param>
        /// <returns>屬性初始值</returns>
        public virtual AttributeValue CalculateInitialValue(List<Attribute> otherAttribute)
        {
            return this.AttributeValue;
        }

        /// <summary>
        /// 屬性經過修飾符後計算的值
        /// </summary>
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
    }
}
