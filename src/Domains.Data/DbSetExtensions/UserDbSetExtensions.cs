using AspNetCoreNuxt.Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Domains.Data.DbSetExtensions
{
    /// <summary>
    /// <see cref="DbSet{TEntity}"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class UserDbSetExtensions
    {
        /// <summary>
        /// 指定されたユーザー ID をもとにユーザー情報を非同期に検索します。
        /// </summary>
        /// <param name="source"><see cref="DbSet{TEntity}"/> オブジェクト。</param>
        /// <param name="id">ユーザー ID。</param>
        /// <param name="queryTrackingBehavior">クエリの追跡動作を表す <see cref="QueryTrackingBehavior"/> 値。</param>
        /// <returns>ユーザー情報を表す <see cref="UserEntity"/> オブジェクト。</returns>
        public static Task<UserEntity> FindByIdAsync(this DbSet<UserEntity> source, int id, QueryTrackingBehavior queryTrackingBehavior)
        {
            return source.Local.SingleOrDefault(x => x.Id == id) is UserEntity entity
                 ? Task.FromResult(entity)
                 : source.AsTracking(queryTrackingBehavior).SingleOrDefaultAsync(x => x.Id == id);
        }

        ///// <summary>
        ///// 指定されたユーザー名をもとにユーザー情報を非同期に検索します。
        ///// </summary>
        ///// <param name="source"><see cref="DbSet{TEntity}"/> オブジェクト。</param>
        ///// <param name="userName">ユーザー名。</param>
        ///// <param name="queryTrackingBehavior">クエリの追跡動作を表す <see cref="QueryTrackingBehavior"/> 値。</param>
        ///// <returns>ユーザー情報を表す <see cref="UserEntity"/> オブジェクト。</returns>
        //public static Task<UserEntity> FindByNameAsync(this DbSet<UserEntity> source, string userName, QueryTrackingBehavior queryTrackingBehavior)
        //{
        //    var normalizedName = source.GetService<ILookupNormalizer>().NormalizeName(userName);
        //    return source.Local.SingleOrDefault(x => x.NormalizedName == normalizedName) is UserEntity entity
        //         ? Task.FromResult(entity)
        //         : source.AsTracking(queryTrackingBehavior).SingleOrDefaultAsync(x => x.NormalizedName == normalizedName);
        //}

        ///// <summary>
        ///// 指定されたユーザー名をもとにユーザーの権限のコレクションを非同期に取得します。
        ///// </summary>
        ///// <param name="source"><see cref="DbSet{TEntity}"/> オブジェクト。</param>
        ///// <param name="userName">ユーザー名。</param>
        ///// <returns>権限を表す <see cref="Permission"/> 値のコレクション。</returns>
        //public static Task<Permission[]> GetPermissionsAsync(this DbSet<UserEntity> source, string userName)
        //{
        //    var normalizedName = source.GetService<ILookupNormalizer>().NormalizeName(userName);
        //    return source.Where(x => x.NormalizedName == normalizedName)
        //                 .SelectMany(x => x.Roles)
        //                 .Select(x => x.Role)
        //                 .SelectMany(x => x.Permissions)
        //                 .Select(x => x.PermissionId)
        //                 .Distinct()
        //                 .ToArrayAsync();
        //}
    }
}
