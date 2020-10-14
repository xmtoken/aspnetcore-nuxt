using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata.Builders
{
    /// <summary>
    /// <see cref="PropertyBuilder{T}"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class PropertyBuilderExtensions
    {
        /// <summary>
        /// プロパティの有効桁数を設定します。
        /// </summary>
        /// <typeparam name="T">プロパティの型。</typeparam>
        /// <param name="builder"><see cref="PropertyBuilder{T}"/> オブジェクト。</param>
        /// <param name="precision">有効桁数。</param>
        /// <returns><see cref="PropertyBuilder{T}"/> オブジェクト。</returns>
        public static PropertyBuilder<T> HasPrecision<T>(this PropertyBuilder<T> builder, int precision)
            => HasPrecision(builder, precision, scale: 0);

        /// <summary>
        /// プロパティの有効桁数と小数点以下桁数を設定します。
        /// </summary>
        /// <typeparam name="T">プロパティの型。</typeparam>
        /// <param name="builder"><see cref="PropertyBuilder{T}"/> オブジェクト。</param>
        /// <param name="precision">有効桁数。</param>
        /// <param name="scale">小数点以下桁数。</param>
        /// <returns><see cref="PropertyBuilder{T}"/> オブジェクト。</returns>
        public static PropertyBuilder<T> HasPrecision<T>(this PropertyBuilder<T> builder, int precision, int scale)
            => builder.HasColumnType($"decimal({precision},{scale})");
    }
}
