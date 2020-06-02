using CsvHelper;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.CsvHelper
{
    /// <summary>
    /// <see cref="CsvReader"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class CsvReaderExtensions
    {
        /// <summary>
        /// CSV ファイルの内容を読み込み指定された型へ変換します。
        /// </summary>
        /// <typeparam name="T">読み込んだデータをマップするクラスの型。</typeparam>
        /// <param name="reader"><see cref="CsvReader"/> オブジェクト。</param>
        /// <returns><see cref="CsvReaderResult{T}"/> オブジェクト。</returns>
        public static CsvReaderResult<T> ReadRecords<T>(this CsvReader reader)
            => ReadRecords<T>(reader, maxHandleException: 100);

        /// <summary>
        /// CSV ファイルの内容を読み込み指定された型へ変換します。
        /// </summary>
        /// <typeparam name="T">読み込んだデータをマップするクラスの型。</typeparam>
        /// <param name="reader"><see cref="CsvReader"/> オブジェクト。</param>
        /// <param name="maxHandleException">処理を中断するまでに許容される例外の最大ハンドル数。</param>
        /// <returns><see cref="CsvReaderResult{T}"/> オブジェクト。</returns>
        public static CsvReaderResult<T> ReadRecords<T>(this CsvReader reader, int maxHandleException)
        {
            var records = new List<T>();
            var breaked = false;
            var exceptions = new List<CsvReaderException>();

            reader.Configuration.BadDataFound = context =>
            {
                var ex = new CsvHelperException(context);
                exceptions.Add(new CsvReaderException(ex));
            };

            reader.Configuration.ReadingExceptionOccurred = ex =>
            {
                exceptions.Add(new CsvReaderException(ex));
                return false;
            };

            try
            {
                while (reader.Read())
                {
                    var record = reader.GetRecord<T>();
                    records.Add(record);

                    if (exceptions.Count > maxHandleException)
                    {
                        breaked = true;
                        break;
                    }
                }
            }
            catch (ParserException)
            {
                throw;
            }

            return new CsvReaderResult<T>(records, exceptions, breaked);
        }
    }
}
