using System;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// LINQ をサポートした仕様パターンを提供します。
    /// </summary>
    /// <typeparam name="T">オブジェクトの型。</typeparam>
    public interface ILinqSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// オブジェクトが仕様を満たしているかどうかを検証する式ツリーを取得します。
        /// </summary>
        /// <returns>オブジェクトが仕様を満たしているかどうかを検証する式ツリー。</returns>
        Expression<Func<T, bool>> GetExpression();
    }
}
