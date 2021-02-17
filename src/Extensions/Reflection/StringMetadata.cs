using System.Reflection;

namespace AspNetCoreNuxt.Extensions.Reflection
{
    /// <summary>
    /// <see cref="string"/> クラスのメタデータを表します。
    /// </summary>
    public static class StringMetadata
    {
        /// <summary>
        /// <see cref="string.CompareTo(string)"/> メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
        /// </summary>
        public static MethodInfo CompareToMethod { get; }
            = typeof(string).GetMethod(nameof(string.CompareTo), new[] { typeof(string) });

        /// <summary>
        /// <see cref="string.Contains(string)"/> メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
        /// </summary>
        public static MethodInfo ContainsMethod { get; }
            = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

        /// <summary>
        /// <see cref="string.EndsWith(string)"/> メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
        /// </summary>
        public static MethodInfo EndsWithMethod { get; }
            = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });

        /// <summary>
        /// <see cref="string.StartsWith(string)"/> メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
        /// </summary>
        public static MethodInfo StartsWithMethod { get; }
            = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
    }
}
