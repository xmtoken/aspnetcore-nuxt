using AspNetCoreNuxt.Applications.WebHost.Core.Authorizations;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 指定されたデータを非同期に登録します。
        /// </summary>
        /// <param name="model">登録するデータを表す <typeparamref name="T"/> オブジェクト。</param>
        /// <returns>登録が成功したかどうかを表す値と、登録されたエンティティのタプルオブジェクト。</returns>
        public virtual Task<(bool Succeeded, T Entity)> AddAsync(T model)
            => AddCoreAsync(model, propertyNames: new[] { "*" });

        /// <summary>
        /// 指定されたデータを非同期に登録します。
        /// </summary>
        /// <param name="model">登録するデータを表す <typeparamref name="T"/> オブジェクト。</param>
        /// <param name="propertyNames">登録するプロパティ名のコレクション。</param>
        /// <returns>登録が成功したかどうかを表す値と、登録されたエンティティのタプルオブジェクト。</returns>
        protected async Task<(bool Succeeded, T Entity)> AddCoreAsync(T model, IEnumerable<string> propertyNames)
        {
            var source = await ExpandoObjectFactory.CreateAsync(
                typeof(T),
                model,
                propertyNames,
                Operations.Create,
                includeUnauthorizedProperty: false,
                includeNullObject: false);

            var entity = Mapper.Map<T>(source);

            await Context.AddAsync(entity);

            var succeeded = await Policy<bool>
                .Handle<DbUpdateException>(ex => ex.IsUniqueConstraintViolation())
                .FallbackAsync(false)
                .ExecuteAsync(async () => await Context.SaveChangesAsync() != 0);

            return (Succeeded: succeeded, Entity: succeeded ? entity : null);
        }
    }
}
