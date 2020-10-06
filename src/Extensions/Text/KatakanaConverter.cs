using AspNetCoreNuxt.Extensions.Collections.Generic;
using AspNetCoreNuxt.Extensions.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// 片仮名の全角と半角を変換するコンバーターを表します。
    /// </summary>
    public class KatakanaConverter
    {
        /// <summary>
        /// 全角の片仮名を抽出する <see cref="Regex"/> オブジェクトを表します。
        /// </summary>
        private readonly Regex FullWidthCharactorRegex;

        /// <summary>
        /// 全角の片仮名をキーとし対応する半角の片仮名を値とするコレクションを表します。
        /// </summary>
        private readonly IReadOnlyDictionary<string, string> FullKeyHalfValueDictionary;

        /// <summary>
        /// 半角の片仮名を抽出する <see cref="Regex"/> オブジェクトを表します。
        /// </summary>
        private readonly Regex HalfWidthCharactorRegex;

        /// <summary>
        /// 半角の片仮名をキーとし対応する全角の片仮名を値とするコレクションを表します。
        /// </summary>
        private readonly IReadOnlyDictionary<string, string> HalfKeyFullValueDictionary;

        /// <summary>
        /// <see cref="KatakanaConverter"/> オブジェクトを取得します。
        /// </summary>
        public static KatakanaConverter Instance { get; } = new KatakanaConverter();

        /// <summary>
        /// <see cref="KatakanaConverter"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        private KatakanaConverter()
        {
            var fullWidthCharactorPattern = string.Join("|", Katakana.FullWidthCharactors);
            FullWidthCharactorRegex = new Regex($"({fullWidthCharactorPattern})", RegexOptions.Compiled);

            FullKeyHalfValueDictionary = Katakana.FullWidthCharactors
                .Zip(Katakana.HalfWidthCharactors, (full, half) => new { Full = full, Half = half })
                .ToDictionary(x => x.Full, x => x.Half)
                .AsReadOnly();

            var halfWidthCharactorPattern = string.Join("|", Katakana.HalfWidthCharactors);
            HalfWidthCharactorRegex = new Regex($"({halfWidthCharactorPattern})", RegexOptions.Compiled);

            HalfKeyFullValueDictionary = Katakana.HalfWidthCharactors
                .Zip(Katakana.FullWidthCharactors, (half, full) => new { Half = half, Full = full })
                .ToDictionary(x => x.Half, x => x.Full)
                .AsReadOnly();
        }

        /// <summary>
        /// 文字列に含まれる半角の片仮名を全角に変換した文字列を返します。
        /// </summary>
        /// <param name="source">変換する文字列。</param>
        /// <returns>変換された文字列。</returns>
        public string ConvertToFullWidthKatakana(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            return HalfWidthCharactorRegex.Replace(source, match
                => match.Success ? HalfKeyFullValueDictionary[match.Value] : match.Value);
        }

        /// <summary>
        /// 文字列に含まれる全角の片仮名を半角に変換した文字列を返します。
        /// </summary>
        /// <param name="source">変換する文字列。</param>
        /// <returns>変換された文字列。</returns>
        public string ConvertToHalfWidthKatakana(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            return FullWidthCharactorRegex.Replace(source, match
                => match.Success ? FullKeyHalfValueDictionary[match.Value] : match.Value);
        }
    }
}
