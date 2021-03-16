using AspNetCoreNuxt.Extensions.AspNetCore.Logging;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using AspNetCoreNuxt.Extensions.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="AggregateQuery{T}"/> クラスのモデルバインダーを表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public class AggregateQueryModelBinder<T> : IModelBinder
    {
        /// <summary>
        /// <see cref="ILogger"/> オブジェクトを表します。
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// <see cref="AggregateQueryModelBinder{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> オブジェクト。</param>
        public AggregateQueryModelBinder(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<AggregateQueryModelBinder<T>>();
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(AggregateQuery<T>.BindingModelName);
            if (valueProviderResult.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(AggregateQuery<T>.Default);
                return Task.CompletedTask;
            }

            var splitValues = valueProviderResult
                .Values
                .ToString()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Where(x => string.IsNullOrWhiteSpace(x) == false)
                .Select(x => x.Trim().ToUpperInvariant())
                .Distinct()
                .ToArray();

            var validValues = splitValues
                .Where(x => x.Contains(".", StringComparison.Ordinal) == false)
                .Where(x => x.StartsWith($"{AggregateOperator.Avg}:", StringComparison.OrdinalIgnoreCase)
                         || x.StartsWith($"{AggregateOperator.Max}:", StringComparison.OrdinalIgnoreCase)
                         || x.StartsWith($"{AggregateOperator.Min}:", StringComparison.OrdinalIgnoreCase)
                         || x.StartsWith($"{AggregateOperator.Sum}:", StringComparison.OrdinalIgnoreCase))
                .Where(x => ExpressionHelper.Validate<T>(x[4..]))
                .Where(x => ExpressionHelper.HasEnumerable<T>(x[4..]) == false)
                .Select(x => new Aggregation(
                    ExpressionHelper.Normalize<T>(x[4..]),
                    Enum.Parse<AggregateOperator>(x[..3], ignoreCase: true)))
                .Where(x => Validate(x.PropertyName, x.AggregateOperator))
                .ToArray();

            if (splitValues.Length != validValues.Length)
            {
                Logger.IncludeInvalidAggregatePredicate(valueProviderResult.Values);
            }

            var model = new AggregateQuery<T>(validValues, valueProviderResult.Values);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 指定されたプロパティ名と集計演算子の組み合わせが有効かどうかを返します。
        /// </summary>
        /// <param name="peropertyName">プロパティ名。</param>
        /// <param name="aggregateOperator">集計演算子を表す <see cref="AggregateOperator"/> 値。</param>
        /// <returns>プロパティ名と集計演算子の組み合わせが有効な場合は true。それ以外の場合は false。</returns>
        private static bool Validate(string peropertyName, AggregateOperator aggregateOperator)
        {
            var property = typeof(T).GetProperty(peropertyName);
            var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            return aggregateOperator switch
            {
                AggregateOperator.Avg
                    => type == typeof(decimal)
                    || type == typeof(double)
                    || type == typeof(float)
                    || type == typeof(int)
                    || type == typeof(long),
                AggregateOperator.Max
                    => type == typeof(decimal)
                    || type == typeof(double)
                    || type == typeof(float)
                    || type == typeof(int)
                    || type == typeof(long)
                    || type == typeof(DateTime)
                    || type == typeof(Guid)
                    || type == typeof(TimeSpan),
                AggregateOperator.Min
                    => type == typeof(decimal)
                    || type == typeof(double)
                    || type == typeof(float)
                    || type == typeof(int)
                    || type == typeof(long)
                    || type == typeof(DateTime)
                    || type == typeof(Guid)
                    || type == typeof(TimeSpan),
                AggregateOperator.Sum
                    => type == typeof(decimal)
                    || type == typeof(double)
                    || type == typeof(float)
                    || type == typeof(int)
                    || type == typeof(long),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
