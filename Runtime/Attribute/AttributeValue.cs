using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memezuki.GameplayAbilitySystem.Attribute
{
    /// <summary>
    /// 屬性值
    /// </summary>
    public struct AttributeValue
    {
        /// <summary>
        /// 屬性
        /// </summary>
        public Attribute Attribute;
        /// <summary>
        /// 基底值
        /// </summary>
        public float BaseValue;
        /// <summary>
        /// 當前值
        /// </summary>
        public float CurrentValue;
        /// <summary>
        /// 修飾符
        /// </summary>
        public AttributeModifier Modifier;
    }
}
