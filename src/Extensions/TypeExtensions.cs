using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="Type"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 具象型かどうかを返します。
        /// </summary>
        /// <param name="type"><see cref="Type"/> オブジェクト。</param>
        /// <returns>具象型の場合は true。それ以外の場合は false。</returns>
        public static bool IsConcrete(this Type type)
            => !type.IsAbstract;

        /// <summary>
        /// <see cref="Nullable{T}"/> 型かどうかを返します。
        /// </summary>
        /// <param name="type"><see cref="Type"/> オブジェクト。</param>
        /// <returns><see cref="Nullable{T}"/> 型の場合は true。それ以外の場合は false。</returns>
        public static bool IsNullable(this Type type)
            => Nullable.GetUnderlyingType(type) is not null;
    }
}
