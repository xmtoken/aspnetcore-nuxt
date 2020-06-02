namespace AspNetCoreNuxt.Extensions.Collections.Generic
{
    /// <summary>
    /// テキストと値のペアを表します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    public class TextValuePair<TValue>
    {
        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 値を取得または設定します。
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// <see cref="TextValuePair{TValue}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public TextValuePair()
        {
        }

        /// <summary>
        /// <see cref="TextValuePair{TValue}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="text">テキスト。</param>
        /// <param name="value">値。</param>
        public TextValuePair(string text, TValue value)
        {
            Text = text;
            Value = value;
        }
    }
}
