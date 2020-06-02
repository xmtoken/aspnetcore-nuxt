using Microsoft.EntityFrameworkCore;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="DbUpdateConcurrencyException"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class DbUpdateConcurrencyExceptionExtensions
    {
        /// <summary>
        /// エラーに関係するすべてのエントリのトラッキングを解除します。
        /// </summary>
        /// <param name="ex"><see cref="DbUpdateConcurrencyException"/> オブジェクト。</param>
        public static void DetachEntries(this DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
