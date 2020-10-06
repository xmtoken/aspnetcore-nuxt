using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="string"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 文字列に含まれる半角の片仮名を全角に変換した文字列を返します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換された文字列。</returns>
        public static string ToFullWidthKatakana(this string value)
            => KatakanaConverter.Instance.ConvertToFullWidthKatakana(value);

        /// <summary>
        /// 文字列に含まれる全角の片仮名を半角に変換した文字列を返します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換された文字列。</returns>
        public static string ToHalfWidthKatakana(this string value)
            => KatakanaConverter.Instance.ConvertToHalfWidthKatakana(value);

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
