namespace AspNetCoreNuxt.Extensions.Identity
{
    /// <summary>
    /// 文字列をハッシュ化する機能を提供します。
    /// </summary>
    public interface IStringHasher
    {
        /// <summary>
        /// 指定された文字列のハッシュ値を返します。
        /// </summary>
        /// <param name="value">ハッシュ値を計算する文字列。</param>
        /// <returns>文字列のハッシュ値。</returns>
        string HashValue(string value);

        /// <summary>
        /// 指定されたハッシュ値と文字列を検証します。
        /// </summary>
        /// <param name="hashedValue">ハッシュ値。</param>
        /// <param name="providedValue">検証する文字列。</param>
        /// <returns>検証が成功した場合は true。それ以外の場合は false。</returns>
        bool VerifyHashedValue(string hashedValue, string providedValue);
    }
}
