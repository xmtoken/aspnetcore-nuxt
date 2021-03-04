using System.Linq;
using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="string"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// キャメルケースへ変換した文字列を返します。
        /// </summary>
        /// <param name="value">パスカルケース、ケバブケースもしくはスネークケースで表現された文字列。</param>
        /// <returns>変換された文字列。</returns>
        public static string ToCamelCase(this string value)
        {
            var values = value.ToKebabCace().Split("-").ToArray();
            var value1 = values.Take(1).SelectMany(val => val.Select(x => char.ToLowerInvariant(x)));
            var value2 = values.Skip(1).SelectMany(val => val.Select(x => x));
            return string.Join(string.Empty, Enumerable.Concat(value1, value2));
        }

        /// <summary>
        /// ケバブケースへ変換した文字列を返します。
        /// </summary>
        /// <param name="value">キャメルケース、パスカルケースもしくはスネークケースで表現された文字列。</param>
        /// <returns>変換された文字列。</returns>
        public static string ToKebabCace(this string value)
        {
            var kebabcase = value;
            kebabcase = Regex.Replace(kebabcase, "([A-Z])([A-Z][a-z])", "$1-$2");
            kebabcase = Regex.Replace(kebabcase, "([a-z])([A-Z])", "$1-$2");
            kebabcase = Regex.Replace(kebabcase, "_", "-");
            return kebabcase;
        }

        /// <summary>
        /// スネークケースへ変換した文字列を返します。
        /// </summary>
        /// <param name="value">キャメルケース、パスカルケースもしくはケバブケースで表現された文字列。</param>
        /// <returns>変換された文字列。</returns>
        public static string ToSnakeCace(this string value)
        {
            var snakecase = value;
            snakecase = Regex.Replace(snakecase, "([A-Z])([A-Z][a-z])", "$1_$2");
            snakecase = Regex.Replace(snakecase, "([a-z])([A-Z])", "$1_$2");
            snakecase = Regex.Replace(snakecase, "-", "_");
            return snakecase;
        }
    }
}
