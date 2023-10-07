namespace Memezuki.GameplayAbilitySystem.Attribute
{
    /// <summary>
    /// 屬性修飾符
    /// </summary>
    public struct AttributeModifier
    {
        /// <summary>
        /// 表示屬性將加上的值
        /// </summary>
        public float Add;

        /// <summary>
        /// 表示屬性將乘上的值
        /// </summary>
        public float Multiply;

        /// <summary>
        /// 表示屬性將被覆蓋的值
        /// </summary>
        public float? Override;

        /// <summary>
        /// 將兩個屬性修飾符結合成一個新的修飾符
        /// </summary>
        /// <param name="other">合併的修飾符</param>
        /// <returns></returns>
        public AttributeModifier Combine(AttributeModifier other)
        {
            other.Add += this.Add;
            other.Multiply += this.Multiply;
            other.Override = this.Override;
            return other;
        }
    }
}
