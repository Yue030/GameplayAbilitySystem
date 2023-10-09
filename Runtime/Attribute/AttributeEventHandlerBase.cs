using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Memezuki.GameplayAbilitySystem.Attribute
{
    /// <summary>
    /// 屬性事件處理器基底類型
    /// </summary>
    public abstract class AttributeEventHandlerBase : ScriptableObject
    {
        /// <summary>
        /// 當屬性基底值被更改時呼叫
        /// </summary>
        /// <param name="attributeSystem">管理屬性的系統</param>
        /// <param name="attribute">屬性</param>
        /// <param name="newValue">新值</param>
        public abstract void PreAttributeBaseChange(AttributeSystem attributeSystem, Attribute attribute, ref AttributeValue newValue);

        /// <summary>
        /// 當屬性當前值被更改時呼叫
        /// </summary>
        /// <param name="attributeSystem">管理屬性的系統</param>
        /// <param name="attribute">屬性</param>
        /// <param name="newValue">新值</param>
        public abstract void PreAttributeChange(AttributeSystem attributeSystem, Attribute attribute, ref AttributeValue newValue);
    }
}
