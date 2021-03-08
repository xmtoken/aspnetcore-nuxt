using CsvHelper;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Extensions.CsvHelper
{
    // TODO:check

    /// <summary>
    /// CSV ファイル読み込み時のエラー情報を表します。
    /// </summary>
    [SuppressMessage("Design", "CA1032:標準例外コンストラクターを実装します")]
    public class CsvReaderException : Exception
    {
        /// <summary>
        /// エラーの原因である項目のインデックスを取得します。
        /// </summary>
        public int CurrentIndex { get; }

        /// <summary>
        /// エラーの原因である項目の文字列を取得します。
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// エラーの原因である項目のヘッダー文字列を取得します。
        /// </summary>
        public string Header { get; }

        /// <summary>
        /// エラーの原因である項目を含む行の文字列を取得します。
        /// </summary>
        public string RawRecord { get; }

        /// <summary>
        /// エラーの原因である項目を含む行のファイルの行番号を取得します。
        /// </summary>
        public int RawRow { get; }

        /// <summary>
        /// エラーの原因である項目を含む行の行番号を取得します。
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// <see cref="CsvReaderException"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="ex"><see cref="CsvHelperException"/> オブジェクト。</param>
        internal CsvReaderException(CsvHelperException ex)
            : base(ex.Message, ex)
        {
            var context = ex.ReadingContext;

            CurrentIndex = context.CurrentIndex;
            Field = GetField(context);
            Header = GetHeader(context);
            RawRecord = context.RawRecord;
            RawRow = context.RawRow;
            Row = context.Row;

            static string GetField(ReadingContext context)
            {
                if (string.IsNullOrEmpty(context.Field))
                {
                    if (0 <= context.CurrentIndex && context.CurrentIndex < context.Record.Length)
                    {
                        return context.Record[context.CurrentIndex];
                    }
                    return null;
                }
                return context.Field;
            }

            static string GetHeader(ReadingContext context)
            {
                if (0 <= context.CurrentIndex && context.CurrentIndex < context.HeaderRecord.Length)
                {
                    return context.HeaderRecord[context.CurrentIndex];
                }
                return null;
            }
        }
    }
}
