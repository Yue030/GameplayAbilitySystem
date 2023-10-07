using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memezuki.GameplayAbilitySystem.Attribute
{
    /// <summary>
    /// 屬性事件處理器基底類型
    /// </summary>
    public abstract class AttributeEventHandlerBase
    {
        /// <summary>
        /// 當屬性基底值被更改時呼叫
        /// </summary>
        /// <param name="attributeSystem">管理屬性的系統</param>
        /// <param name="attribute">屬性</param>
        /// <param name="newValue">新值</param>
        public abstract void PreAttributeBaseChange(AttributeSystem attributeSystem, Attribute attribute, ref float newValue);

        /// <summary>
        /// 當屬性當前值被更改時呼叫
        /// </summary>
        /// <param name="attributeSystem">管理屬性的系統</param>
        /// <param name="attribute">屬性</param>
        /// <param name="newValue">新值</param>
        public abstract void PreAttributeChange(AttributeSystem attributeSystem, Attribute attribute, ref float newValue);
    }
}
