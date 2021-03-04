using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="Type"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// <see cref="Nullable{T}"/> 型かどうかを返します。
        /// </summary>
        /// <param name="type"><see cref="Type"/> オブジェクト。</param>
        /// <returns><see cref="Nullable{T}"/> 型の場合は true。それ以外の場合は false。</returns>
        public static bool IsNullable(this Type type)
            => Nullable.GetUnderlyingType(type) is not null;
    }
}
