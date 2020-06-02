using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore
{
    /// <summary>
    /// クエリ文字列のコンバーターを表します。
    /// </summary>
    public static class QueryStringConverter
    {
        /// <summary>
        /// 指定された文字列を <see cref="bool"/> 型へ変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換に成功した場合は変換された値。それ以外の場合は null。</returns>
        public static bool? ConvertToBoolean(string value)
            => bool.TryParse(value, out var parsed) ? parsed : (bool?)null;

        /// <summary>
        /// 指定された文字列のコレクションを <see cref="bool"/> 型のコレクションへ変換します。
        /// </summary>
        /// <param name="values">変換する文字列のコレクション。</param>
        /// <returns>変換に成功した値のみを含むコレクション。</returns>
        public static IEnumerable<bool> ConvertToBoolean(IEnumerable<string> values)
        {
            if (values == null)
            {
                yield break;
            }
            foreach (var value in values)
            {
                if (bool.TryParse(value, out var parsed))
                {
                    yield return parsed;
                }
            }
        }

        /// <summary>
        /// 指定された文字列を <see cref="DateTime"/> 型へ変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換に成功した場合は変換された値。それ以外の場合は null。</returns>
        public static DateTime? ConvertToDateTime(string value)
            => DateTime.TryParse(value, out var parsed) ? parsed : (DateTime?)null;

        /// <summary>
        /// 指定された文字列のコレクションを <see cref="DateTime"/> 型のコレクションへ変換します。
        /// </summary>
        /// <param name="values">変換する文字列のコレクション。</param>
        /// <returns>変換に成功した値のみを含むコレクション。</returns>
        public static IEnumerable<DateTime> ConvertToDateTime(IEnumerable<string> values)
        {
            if (values == null)
            {
                yield break;
            }
            foreach (var value in values)
            {
                if (DateTime.TryParse(value, out var parsed))
                {
                    yield return parsed;
                }
            }
        }

        /// <summary>
        /// 指定された文字列を <see cref="decimal"/> 型へ変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換に成功した場合は変換された値。それ以外の場合は null。</returns>
        public static decimal? ConvertToDecimal(string value)
            => decimal.TryParse(value, out var parsed) ? parsed : (decimal?)null;

        /// <summary>
        /// 指定された文字列のコレクションを <see cref="decimal"/> 型のコレクションへ変換します。
        /// </summary>
        /// <param name="values">変換する文字列のコレクション。</param>
        /// <returns>変換に成功した値のみを含むコレクション。</returns>
        public static IEnumerable<decimal> ConvertToDecimal(IEnumerable<string> values)
        {
            if (values == null)
            {
                yield break;
            }
            foreach (var value in values)
            {
                if (decimal.TryParse(value, out var parsed))
                {
                    yield return parsed;
                }
            }
        }

        /// <summary>
        /// 指定された文字列を <see cref="double"/> 型へ変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換に成功した場合は変換された値。それ以外の場合は null。</returns>
        public static double? ConvertToDouble(string value)
            => double.TryParse(value, out var parsed) ? parsed : (double?)null;

        /// <summary>
        /// 指定された文字列のコレクションを <see cref="double"/> 型のコレクションへ変換します。
        /// </summary>
        /// <param name="values">変換する文字列のコレクション。</param>
        /// <returns>変換に成功した値のみを含むコレクション。</returns>
        public static IEnumerable<double> ConvertToDouble(IEnumerable<string> values)
        {
            if (values == null)
            {
                yield break;
            }
            foreach (var value in values)
            {
                if (double.TryParse(value, out var parsed))
                {
                    yield return parsed;
                }
            }
        }

        /// <summary>
        /// 指定された文字列を <typeparamref name="TEnum"/> 型へ変換します。
        /// </summary>
        /// <typeparam name="TEnum">列挙体の型。</typeparam>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換に成功した場合は変換された値。それ以外の場合は null。</returns>
        public static TEnum? ConvertToEnum<TEnum>(string value)
            where TEnum : struct, Enum
            => Enum.TryParse<TEnum>(value, ignoreCase: true, out var parsed) ? parsed : (TEnum?)null;

        /// <summary>
        /// 指定された文字列のコレクションを <typeparamref name="TEnum"/> 型のコレクションへ変換します。
        /// </summary>
        /// <typeparam name="TEnum">列挙体の型。</typeparam>
        /// <param name="values">変換する文字列のコレクション。</param>
        /// <returns>変換に成功した値のみを含むコレクション。</returns>
        public static IEnumerable<TEnum> ConvertToEnum<TEnum>(IEnumerable<string> values)
            where TEnum : struct, Enum
        {
            if (values == null)
            {
                yield break;
            }
            foreach (var value in values)
            {
                if (Enum.TryParse<TEnum>(value, ignoreCase: true, out var parsed))
                {
                    yield return parsed;
                }
            }
        }

        /// <summary>
        /// 指定された文字列を <see cref="int"/> 型へ変換します。
        /// </summary>
        /// <param name="value">変換する文字列。</param>
        /// <returns>変換に成功した場合は変換された値。それ以外の場合は null。</returns>
        public static int? ConvertToInt32(string value)
            => int.TryParse(value, out var parsed) ? parsed : (int?)null;

        /// <summary>
        /// 指定された文字列のコレクションを <see cref="int"/> 型のコレクションへ変換します。
        /// </summary>
        /// <param name="values">変換する文字列のコレクション。</param>
        /// <returns>変換に成功した値のみを含むコレクション。</returns>
        public static IEnumerable<int> ConvertToInt32(IEnumerable<string> values)
        {
            if (values == null)
            {
                yield break;
            }
            foreach (var value in values)
            {
                if (int.TryParse(value, out var parsed))
                {
                    yield return parsed;
                }
            }
        }
    }
}
