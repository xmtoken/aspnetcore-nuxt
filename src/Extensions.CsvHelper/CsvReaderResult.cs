using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.CsvHelper
{
    /// <summary>
    /// CSV ファイル読み込みの結果を表します。
    /// </summary>
    /// <typeparam name="T">読み込んだデータをマップするクラスの型。</typeparam>
    public class CsvReaderResult<T>
    {
        /// <summary>
        /// 例外の最大ハンドル数を超過して処理が中断されたかどうかを取得します。
        /// </summary>
        public bool Breaked { get; }

        /// <summary>
        /// <see cref="CsvReaderException"/> オブジェクトのコレクションを取得します。
        /// </summary>
        public ReadOnlyCollection<CsvReaderException> Exceptions { get; }

        /// <summary>
        /// CSV ファイルの内容を変換した <typeparamref name="T"/> オブジェクトのコレクションを取得します。
        /// </summary>
        public ReadOnlyCollection<T> Records { get; }

        /// <summary>
        /// CSV ファイルの読み込みに成功したかどうかを取得します。
        /// </summary>
        public bool Succeeded { get; }

        /// <summary>
        /// <see cref="CsvReaderResult{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="records">CSV ファイルの内容を変換した <typeparamref name="T"/> オブジェクトのコレクション。</param>
        /// <param name="exceptions"><see cref="CsvReaderException"/> オブジェクトのコレクション。</param>
        /// <param name="breaked">例外の最大ハンドル数を超過して処理が中断された場合は true。それ以外の場合は false。</param>
        public CsvReaderResult(IEnumerable<T> records, IEnumerable<CsvReaderException> exceptions, bool breaked)
        {
            Breaked = breaked;
            Exceptions = new ReadOnlyCollection<CsvReaderException>(exceptions.ToArray());
            Records = new ReadOnlyCollection<T>(records.ToArray());
            Succeeded = exceptions.Any();
        }
    }
}
