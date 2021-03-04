using System;
using System.ComponentModel;

namespace AspNetCoreNuxt.Extensions.ComponentModel
{
    /// <summary>
    /// <see cref="TypeConverter"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class TypeConverterExtensions
    {
        /// <summary>
        /// 指定された文字列を指定された型へ変換します。
        /// </summary>
        /// <typeparam name="T">変換する型。</typeparam>
        /// <param name="converter"><see cref="TypeConverter"/> オブジェクト。</param>
        /// <param name="text">変換する文字列。</param>
        /// <param name="result">変換された値。</param>
        /// <returns>変換に成功した場合は true。それ以外の場合は false。</returns>
        public static bool TryConvertFromString<T>(this TypeConverter converter, string text, out T result)
        {
            try
            {
                result = (T)converter.ConvertFromString(text);
                return true;
            }
            catch (ArgumentException)
            {
                result = default;
                return false;
            }
            catch (FormatException)
            {
                result = default;
                return false;
            }
        }
    }
}
