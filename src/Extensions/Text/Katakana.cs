using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.Text
{
    /// <summary>
    /// 片仮名に関する情報を表します。
    /// </summary>
    public static class Katakana
    {
        /// <summary>
        /// 全角の片仮名のコレクションを取得します。
        /// </summary>
        public static IReadOnlyCollection<string> FullWidthCharactors { get; } = Array.AsReadOnly(new[]
        {
            "ヺ", "ヴ",
            "ガ", "ギ", "グ", "ゲ", "ゴ",
            "ザ", "ジ", "ズ", "ゼ", "ゾ",
            "ダ", "ヂ", "ヅ", "デ", "ド",
            "バ", "ビ", "ブ", "ベ", "ボ",
            "ヷ",
            "パ", "ピ", "プ", "ペ", "ポ",
            "　",
            "。", "「", "」", "、", "・",
            "ヲ",
            "ァ", "ィ", "ゥ", "ェ", "ォ",
            "ャ", "ュ", "ョ", "ッ", "ー",
            "ア", "イ", "ウ", "エ", "オ",
            "カ", "キ", "ク", "ケ", "コ",
            "サ", "シ", "ス", "セ", "ソ",
            "タ", "チ", "ツ", "テ", "ト",
            "ナ", "ニ", "ヌ", "ネ", "ノ",
            "ハ", "ヒ", "フ", "ヘ", "ホ",
            "マ", "ミ", "ム", "メ", "モ",
            "ヤ", "ユ", "ヨ",
            "ラ", "リ", "ル", "レ", "ロ",
            "ワ", "ン",
            "゛", "゜",
        });

        /// <summary>
        /// 半角の片仮名のコレクションを取得します。
        /// </summary>
        public static IReadOnlyCollection<string> HalfWidthCharactors { get; } = Array.AsReadOnly(new[]
        {
            "ｦﾞ", "ｳﾞ",
            "ｶﾞ", "ｷﾞ", "ｸﾞ", "ｹﾞ", "ｺﾞ",
            "ｻﾞ", "ｼﾞ", "ｽﾞ", "ｾﾞ", "ｿﾞ",
            "ﾀﾞ", "ﾁﾞ", "ﾂﾞ", "ﾃﾞ", "ﾄﾞ",
            "ﾊﾞ", "ﾋﾞ", "ﾌﾞ", "ﾍﾞ", "ﾎﾞ",
            "ﾜﾞ",
            "ﾊﾟ", "ﾋﾟ", "ﾌﾟ", "ﾍﾟ", "ﾎﾟ",
            " ",
            "｡", "｢", "｣", "､", "･",
            "ｦ",
            "ｧ", "ｨ", "ｩ", "ｪ", "ｫ",
            "ｬ", "ｭ", "ｮ", "ｯ", "ｰ",
            "ｱ", "ｲ", "ｳ", "ｴ", "ｵ",
            "ｶ", "ｷ", "ｸ", "ｹ", "ｺ",
            "ｻ", "ｼ", "ｽ", "ｾ", "ｿ",
            "ﾀ", "ﾁ", "ﾂ", "ﾃ", "ﾄ",
            "ﾅ", "ﾆ", "ﾇ", "ﾈ", "ﾉ",
            "ﾊ", "ﾋ", "ﾌ", "ﾍ", "ﾎ",
            "ﾏ", "ﾐ", "ﾑ", "ﾒ", "ﾓ",
            "ﾔ", "ﾕ", "ﾖ",
            "ﾗ", "ﾘ", "ﾙ", "ﾚ", "ﾛ",
            "ﾜ", "ﾝ",
            "ﾞ", "ﾟ",
        });
    }
}
