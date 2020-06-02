using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="string"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 指定された文字列をケバブケースへ変換します。
        /// </summary>
        /// <param name="value">パスカルケースもしくはスネークケースで表現された文字列。</param>
        /// <returns>ケバブケースで表現された文字列。</returns>
        public static string ToKebabCace(this string value)
        {
            var kebabcase = value;
            kebabcase = Regex.Replace(kebabcase, "([A-Z])([A-Z][a-z])", "$1-$2");
            kebabcase = Regex.Replace(kebabcase, "([a-z])([A-Z])", "$1-$2");
            kebabcase = Regex.Replace(kebabcase, "_", "-");
            return kebabcase;
        }

        /// <summary>
        /// 指定された文字列をスネークケースへ変換します。
        /// </summary>
        /// <param name="value">パスカルケースもしくはケバブケースで表現された文字列。</param>
        /// <returns>スネークケースで表現された文字列。</returns>
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
