using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memezuki.GameplayAbilitySystem.Tag
{
    /// <summary>
    /// 遊戲標籤資料
    /// </summary>
    [Serializable]
    public struct GameplayTagData
    {
        /// <summary>
        /// 標籤代號
        /// </summary>
        public int Tag;

        /// <summary>
        /// 標籤父層關係
        /// </summary>
        public int[] Ancestors;

        /// <summary>
        /// 確認此標籤資料是否繼承於 <paramref name="other"/> 
        /// </summary>
        /// <param name="other">確認的對象</param>
        /// <returns>此標籤資料是否繼承於 <paramref name="other"/> </returns>
        public bool IsDescendantOf(GameplayTagData other)
        {
            return this.Ancestors.Contains(other.Tag);
        }

        /// <summary>
        /// 比較 <paramref name="obj"/> 與標籤資料是否相同
        /// </summary>
        /// <param name="obj">比較的物件</param>
        /// <returns><paramref name="obj"/> 與標籤資料是否相同</returns>
        public override bool Equals(object obj)
        {
            return obj is GameplayTagData && this == (GameplayTagData)obj;
        }

        /// <summary>
        /// 取得此標籤資料的 HashCode
        /// </summary>
        /// <returns>此標籤的 HashCode</returns>
        public override int GetHashCode()
        {
            return this.Tag.GetHashCode();
        }

        /// <summary>
        /// 比較兩個標籤資料是否相同
        /// </summary>
        /// <param name="x">第一個標籤資料</param>
        /// <param name="y">第二個標籤資料</param>
        /// <returns>標籤是否相同</returns>
        public static bool operator ==(GameplayTagData x, GameplayTagData y)
        {
            return x.Tag == y.Tag;
        }

        /// <summary>
        /// 比較兩個標籤資料是否不一樣
        /// </summary>
        /// <param name="x">第一個標籤資料</param>
        /// <param name="y">第二個標籤資料</param>
        /// <returns>標籤是否不一樣</returns>
        public static bool operator !=(GameplayTagData x, GameplayTagData y)
        {
            return !(x == y);
        }
    }
}
