namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// 仕様パターンを提供します。
    /// </summary>
    /// <typeparam name="T">オブジェクトの型。</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// 指定されたオブジェクトが仕様を満たしているかどうかを返します。
        /// </summary>
        /// <param name="obj">検証するオブジェクト。</param>
        /// <returns>オブジェクトが仕様を満たしている場合は true。それ以外の場合は false。</returns>
        bool IsSatisfiedBy(T obj);
    }
}
