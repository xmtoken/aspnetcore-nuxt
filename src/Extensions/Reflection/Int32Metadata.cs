using System.Reflection;

namespace AspNetCoreNuxt.Extensions.Reflection
{
    /// <summary>
    /// <see cref="int"/> 構造体のメタデータを表します。
    /// </summary>
    public static class Int32Metadata
    {
        /// <summary>
        /// <see cref="int.CompareTo(int)"/> メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
        /// </summary>
        public static MethodInfo CompareToMethod { get; }
            = typeof(int).GetMethod(nameof(int.CompareTo), new[] { typeof(int) });
    }
}
