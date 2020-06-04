using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="DbUpdateException"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class DbUpdateExceptionExtensions
    {
        /// <summary>
        /// 一意制約違反の例外かどうかを返します。
        /// </summary>
        /// <param name="ex"><see cref="DbUpdateException"/> オブジェクト。</param>
        /// <returns>一意制約違反の例外の場合は true。それ以外の場合は false。</returns>
        public static bool IsUniqueConstraintViolation(this DbUpdateException ex)
            => ex.InnerException is OracleException exception && exception.Number == 1;
    }
}
