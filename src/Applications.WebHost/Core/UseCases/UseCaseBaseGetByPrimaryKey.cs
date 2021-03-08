using AspNetCoreNuxt.Applications.WebHost.Core.Linq;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using AspNetCoreNuxt.Extensions.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 指定された主キーに一致するデータを非同期に取得します。
        /// </summary>
        /// <param name="keyValues">取得するデータの主キー値のコレクション。</param>
        /// <param name="field"><see cref="IFieldQuery{T}"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>主キーに一致するデータ。</returns>
        public virtual Task<T> GetByPrimaryKeyAsync(object[] keyValues, IFieldQuery<T> field, CancellationToken cancellationToken = default)
            => GetByPrimaryKeyCoreAsync(Context.Set<T>(), keyValues, field, cancellationToken);

        /// <summary>
        /// 指定された主キーに一致するデータを非同期に取得します。
        /// </summary>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="keyValues">取得するデータの主キー値のコレクション。</param>
        /// <param name="field"><see cref="IFieldQuery{T}"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>主キーに一致するデータ。</returns>
        protected Task<T> GetByPrimaryKeyCoreAsync(IQueryable<T> source, object[] keyValues, IFieldQuery<T> field, CancellationToken cancellationToken = default)
        {
            var linq = source.AsQueryable();

            var index = 0;
            foreach (var property in Context.Set<T>().EntityType.FindPrimaryKey().Properties)
            {
                var parameterExpression = Expression.Parameter(typeof(T));
                var leftExpression = Expression.Property(parameterExpression, property.PropertyInfo);
                var rightParameterizedExpression = ExpressionHelper.Parameterize(keyValues[index++]);
                var rightExpression = Expression.Convert(rightParameterizedExpression, property.ClrType);
                var bodyExpression = Expression.Equal(leftExpression, rightExpression);
                var expression = Expression.Lambda<Func<T, bool>>(bodyExpression, parameterExpression);
                linq = linq.Where(expression);
            }

            return linq
                .TryAddProjectionQuery(Mapper, field)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
