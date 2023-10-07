using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Memezuki.GameplayAbilitySystem.Tag
{
    /// <summary>
    /// 遊戲標籤
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay Ability System/Tag")]
    public class GameplayTag : ScriptableObject
    {
        /// <summary>
        /// 父遊戲標籤
        /// </summary>
        [SerializeField]
        private GameplayTag _parent;

        /// <summary>
        /// 標籤資料
        /// </summary>
        public GameplayTagData TagData;

        public void OnValidate()
        {
            this.TagData = this.Build();
        }

        /// <summary>
        /// 確認此遊戲標籤是否繼承 <paramref name="other"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns>此遊戲標籤是否繼承於 <paramref name="other"/></returns>
        public bool IsDescendantOf(GameplayTag other)
        {
            GameplayTag tag = this._parent;

            while (tag != null)
            {
                if (tag == other)
                {
                    return true;
                }

                tag = tag._parent;
            }

            return false;
        }

        /// <summary>
        /// 建立此遊戲標籤的標籤資料
        /// </summary>
        /// <returns>此遊戲標籤的標籤資料</returns>
        public GameplayTagData Build()
        {
            List<int> ancestors = new List<int>();
            GameplayTag parent = this._parent;

            while (parent != null)
            {
                ancestors.Add(parent.GetInstanceID());
                parent = parent._parent;
            }

            return new GameplayTagData()
            {
                Tag = this.GetInstanceID(),
                Ancestors = ancestors.ToArray()
            };
        }

        
    }
}
