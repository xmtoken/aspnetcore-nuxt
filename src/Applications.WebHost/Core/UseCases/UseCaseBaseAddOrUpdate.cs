using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 指定された主キーに一致するデータを非同期に登録もしくは更新します。
        /// </summary>
        /// <param name="keyValues">登録もしくは更新するデータの主キー値のコレクション。</param>
        /// <param name="model">登録もしくは更新するデータを表す <typeparamref name="T"/> オブジェクト。</param>
        /// <returns>登録もしくは更新が成功したかどうかを表す値と、登録もしくは更新されたエンティティのタプルオブジェクト。</returns>
        public virtual Task<(bool Succeeded, T Entity)> AddOrUpdateAsync(object[] keyValues, T model)
            => AddOrUpdateCoreAsync(keyValues, model);

        /// <summary>
        /// 指定された主キーに一致するデータを非同期に登録もしくは更新します。
        /// </summary>
        /// <param name="keyValues">登録もしくは更新するデータの主キー値のコレクション。</param>
        /// <param name="model">登録もしくは更新するデータを表す <typeparamref name="T"/> オブジェクト。</param>
        /// <returns>登録もしくは更新が成功したかどうかを表す値と、登録もしくは更新されたエンティティのタプルオブジェクト。</returns>
        protected async Task<(bool Succeeded, T Entity)> AddOrUpdateCoreAsync(object[] keyValues, T model)
        {
            var result = await AddAsync(model);
            if (result.Succeeded)
            {
                return result;
            }
            Context.ChangeTracker.Clear();
            return await UpdateAsync(keyValues, model);
        }
    }
}
