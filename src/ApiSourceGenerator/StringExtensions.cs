using System.Linq;
using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.ApiSourceGenerator
{
    /// <summary>
    /// <see cref="string"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// キャメルケースへ変換した文字列を返します。
        /// </summary>
        /// <param name="value">パスカルケースで表現された文字列。</param>
        /// <returns>変換された文字列。</returns>
        public static string ToCamelCase(this string value)
        {
            var values = ToKebabCace(value).Split('-').ToArray();
            return string.Join(string.Empty, values.Take(1).Select(x => x.ToLowerInvariant()).Concat(values.Skip(1)));
        }

        /// <summary>
        /// ケバブケースへ変換した文字列を返します。
        /// </summary>
        /// <param name="value">パスカルケースもしくはスネークケースで表現された文字列。</param>
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
        /// <param name="value">パスカルケースもしくはケバブケースで表現された文字列。</param>
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
