using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memezuki.GameplayAbilitySystem
{
    /// <summary>
    /// 描述持續時間類型的列舉
    /// </summary>
    public enum DurationPolicyEnum
    {
        /// <summary>
        /// 立即生效
        /// </summary>
        Intant = 1,
        /// <summary>
        /// 無限時間
        /// </summary>
        Infinite = 2,
        /// <summary>
        /// 指定時間
        /// </summary>
        Duration = 3
    }
}
