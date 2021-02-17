using System;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.Reflection
{
    /// <summary>
    /// <see cref="Guid"/> 構造体のメタデータを表します。
    /// </summary>
    public static class GuidMetadata
    {
        /// <summary>
        /// <see cref="Guid.CompareTo(Guid)"/> メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
        /// </summary>
        public static MethodInfo CompareToMethod { get; }
            = typeof(Guid).GetMethod(nameof(Guid.CompareTo), new[] { typeof(Guid) });
    }
}
