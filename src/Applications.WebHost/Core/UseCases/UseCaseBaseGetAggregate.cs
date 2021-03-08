using AspNetCoreNuxt.Applications.WebHost.Core.Linq;
using AspNetCoreNuxt.Extensions;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using AspNetCoreNuxt.Extensions.Linq.Emit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// <see cref="Enumerable"/> クラスの集計メソッドを表します。
        /// </summary>
        private static class EnumerableAggregateMethod
        {
            /// <summary>
            /// <see cref="MethodInfo"/> オブジェクトのキャッシュを表します。
            /// </summary>
            private static readonly ConcurrentDictionary<(string MethodName, Type PropertyType), MethodInfo> Cache = new();

            /// <summary>
            /// <see cref="Enumerable"/> クラスの Average メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
            /// </summary>
            /// <param name="propertyType">集計する値の型。</param>
            /// <returns><see cref="MethodInfo"/> オブジェクト。</returns>
            public static MethodInfo GetAvg(Type propertyType)
                => GetCore(nameof(Enumerable.Average), propertyType);

            /// <summary>
            /// <see cref="Enumerable"/> クラスの Max メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
            /// </summary>
            /// <param name="propertyType">集計する値の型。</param>
            /// <returns><see cref="MethodInfo"/> オブジェクト。</returns>
            public static MethodInfo GetMax(Type propertyType)
                => GetCore(nameof(Enumerable.Max), propertyType);

            /// <summary>
            /// <see cref="Enumerable"/> クラスの Min メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
            /// </summary>
            /// <param name="propertyType">集計する値の型。</param>
            /// <returns><see cref="MethodInfo"/> オブジェクト。</returns>
            public static MethodInfo GetMin(Type propertyType)
                => GetCore(nameof(Enumerable.Min), propertyType);

            /// <summary>
            /// <see cref="Enumerable"/> クラスの Sum メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
            /// </summary>
            /// <param name="propertyType">集計する値の型。</param>
            /// <returns><see cref="MethodInfo"/> オブジェクト。</returns>
            public static MethodInfo GetSum(Type propertyType)
                => GetCore(nameof(Enumerable.Sum), propertyType);

            /// <summary>
            /// <see cref="Enumerable"/> クラスの集計メソッドの <see cref="MethodInfo"/> オブジェクトを取得します。
            /// </summary>
            /// <param name="methodName">取得するメソッド名。</param>
            /// <param name="propertyType">集計する値の型。</param>
            /// <returns><see cref="MethodInfo"/> オブジェクト。</returns>
            private static MethodInfo GetCore(string methodName, Type propertyType)
                => Cache.GetOrAdd((methodName, propertyType), key =>
                {
                    return typeof(Enumerable)
                        .GetMethods(BindingFlags.Static | BindingFlags.Public)
                        .Where(x => x.Name == key.MethodName)
                        .Where(x => x.GetParameters().Length == 2)
                        .Where(x => x.GetParameters()[0].Name == "source")
                        .Where(x => x.GetParameters()[1].Name == "selector")
                        .Where(x => x.GetParameters()[1].ParameterType.GenericTypeArguments[1] == key.PropertyType)
                        .Single()
                        .MakeGenericMethod(typeof(T));
                });
        }

        /// <summary>
        /// GroupBy メソッドのジェネリックパラメーターがアンバウンドな <see cref="MethodInfo"/> オブジェクトを表します。
        /// </summary>
        /// <remarks><see cref="Queryable.GroupBy{TSource, TKey, TResult}(IQueryable{TSource}, Expression{Func{TSource, TKey}}, Expression{Func{TKey, IEnumerable{TSource}, TResult}})"/></remarks>
        private static readonly MethodInfo QueryableGroupByGenericMethod
            = typeof(Queryable)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.Name == nameof(Queryable.GroupBy))
                .Where(x => x.GetParameters().Length == 3)
                .Where(x => x.GetParameters()[0].Name == "source")
                .Where(x => x.GetParameters()[1].Name == "keySelector")
                .Where(x => x.GetParameters()[2].Name == "resultSelector")
                .Single();

        /// <summary>
        /// 指定された条件に一致するデータを非同期に集計して取得します。
        /// </summary>
        /// <param name="specification"><see cref="SearchSpecification{T}"/> オブジェクト。</param>
        /// <param name="group"><see cref="IGroupQuery{T}"/> オブジェクト。</param>
        /// <param name="aggregate"><see cref="IAggregateQuery{T}"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>条件に一致する集計データのコレクション。</returns>
        public virtual Task<object[]> GetAggregateAsync(SearchSpecification<T> specification, IGroupQuery<T> group, IAggregateQuery<T> aggregate, CancellationToken cancellationToken = default)
            => GetAggregateCoreAsync(Context.Set<T>(), specification, group, aggregate, cancellationToken);

        /// <summary>
        /// 指定された条件に一致するデータを非同期に集計して取得します。
        /// </summary>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="specification"><see cref="SearchSpecification{T}"/> オブジェクト。</param>
        /// <param name="group"><see cref="IGroupQuery{T}"/> オブジェクト。</param>
        /// <param name="aggregate"><see cref="IAggregateQuery{T}"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>条件に一致する集計データのコレクション。</returns>
        protected Task<object[]> GetAggregateCoreAsync(IQueryable<T> source, SearchSpecification<T> specification, IGroupQuery<T> group, IAggregateQuery<T> aggregate, CancellationToken cancellationToken = default)
        {
            var groupKeyProperties = group
                .PropertyNames
                .Select(propertyName => typeof(T).GetProperty(propertyName))
                .ToArray();

            if (groupKeyProperties.Length == 0)
            {
                return Task.FromResult(Array.Empty<object>());
            }

            var aggregateProperties = aggregate
                .GetAggregations()
                .Select(aggregation => typeof(T).GetProperty(aggregation.PropertyName))
                .ToArray();

            var anonymousTypeProperties = new Dictionary<string, Type>();
            foreach (var property in Enumerable.Concat(groupKeyProperties, aggregateProperties).Distinct())
            {
                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                if (propertyType == typeof(double) ||
                    propertyType == typeof(float) ||
                    propertyType == typeof(int) ||
                    propertyType == typeof(decimal))
                {
                    propertyType = property.PropertyType.IsNullable() ? typeof(decimal?) : typeof(decimal);
                    anonymousTypeProperties.Add(property.Name, propertyType);
                }
                else
                {
                    anonymousTypeProperties.Add(property.Name, property.PropertyType);
                }
            }
            var anonymousType = RuntimeTypeBuilder.CreateDynamicType(anonymousTypeProperties);

            var groupKeyTupleGenericTypeArguments = groupKeyProperties.Select(x => x.PropertyType).ToArray();
            var groupKeyTupleUnboundType
                = groupKeyTupleGenericTypeArguments.Length == 1 ? typeof(Tuple<>)
                : groupKeyTupleGenericTypeArguments.Length == 2 ? typeof(Tuple<,>)
                : groupKeyTupleGenericTypeArguments.Length == 3 ? typeof(Tuple<,,>)
                : groupKeyTupleGenericTypeArguments.Length == 4 ? typeof(Tuple<,,,>)
                : groupKeyTupleGenericTypeArguments.Length == 5 ? typeof(Tuple<,,,,>)
                : groupKeyTupleGenericTypeArguments.Length == 6 ? typeof(Tuple<,,,,,>)
                : groupKeyTupleGenericTypeArguments.Length == 7 ? typeof(Tuple<,,,,,,>)
                : throw new NotImplementedException();
            var groupKeyTupleType = groupKeyTupleUnboundType.MakeGenericType(groupKeyTupleGenericTypeArguments);
            var groupKeyTupleConstructor = groupKeyTupleType.GetConstructors().Single();

            var keySelectorParameterExpression = Expression.Parameter(typeof(T), "x");
            var keySelectorPropertiesExpressions = groupKeyProperties.Select(x => Expression.Property(keySelectorParameterExpression, x.Name));
            var keySelectorNewExpression = Expression.New(groupKeyTupleConstructor, keySelectorPropertiesExpressions);
            var keySelectorExpression = Expression.Lambda(keySelectorNewExpression, keySelectorParameterExpression);

            var bindings = new List<MemberAssignment>();
            var resultSelectorKeyParameterExpression = Expression.Parameter(groupKeyTupleType, "key");
            var resultSelectorSourceParameterExpression = Expression.Parameter(typeof(IEnumerable<T>), "source");

            foreach (var aggregation in aggregate.GetAggregations())
            {
                if (groupKeyProperties.Any(x => x.Name == aggregation.PropertyName))
                {
                    continue;
                }

                var aggregateProperty = anonymousType.GetProperty(aggregation.PropertyName);
                var aggregateMethod = aggregation.AggregateOperator switch
                {
                    AggregateOperator.Avg => EnumerableAggregateMethod.GetAvg(aggregateProperty.PropertyType),
                    AggregateOperator.Max => EnumerableAggregateMethod.GetMax(aggregateProperty.PropertyType),
                    AggregateOperator.Min => EnumerableAggregateMethod.GetMin(aggregateProperty.PropertyType),
                    AggregateOperator.Sum => EnumerableAggregateMethod.GetSum(aggregateProperty.PropertyType),
                    _ => throw new NotImplementedException(),
                };

                var aggregateFunctionParameterExpression = Expression.Parameter(typeof(T), "x");
                var aggregateFunctionPropertyExpression = Expression.Property(aggregateFunctionParameterExpression, aggregation.PropertyName);
                var aggregateFunctionPropertyConvertExpression = Expression.Convert(aggregateFunctionPropertyExpression, aggregateProperty.PropertyType);
                var aggregateFunctionExpression = Expression.Lambda(aggregateFunctionPropertyConvertExpression, aggregateFunctionParameterExpression);

                var aggregateCallExpression = Expression.Call(aggregateMethod, resultSelectorSourceParameterExpression, aggregateFunctionExpression);
                var assignmentExpression = Expression.Bind(aggregateProperty, aggregateCallExpression);
                bindings.Add(assignmentExpression);
            }

            for (var i = 0; i < groupKeyProperties.Length; i++)
            {
                var groupKeyProperty = groupKeyProperties[i];
                var anonymousTypeProperty = anonymousType.GetProperty(groupKeyProperty.Name);
                var groupKeyPropertyExpression = Expression.Property(resultSelectorKeyParameterExpression, $"Item{i + 1}");
                var groupKeyPropertyConvertExpression = Expression.Convert(groupKeyPropertyExpression, anonymousTypeProperty.PropertyType);
                var assignmentExpression = Expression.Bind(anonymousTypeProperty, groupKeyPropertyConvertExpression);
                bindings.Add(assignmentExpression);
            }

            var resultSelectorNewExpression = Expression.New(anonymousType);
            var resultSelectorInitExpression = Expression.MemberInit(resultSelectorNewExpression, bindings);
            var resultSelectorExpression = Expression.Lambda(resultSelectorInitExpression, new[]
            {
                resultSelectorKeyParameterExpression,
                resultSelectorSourceParameterExpression,
            });

            var groupBySourceType = typeof(T);
            var groupByKeyType = groupKeyTupleType;
            var groupByResultType = anonymousType;
            var groupByMethod = QueryableGroupByGenericMethod
                .MakeGenericMethod(new[]
                {
                    groupBySourceType,
                    groupByKeyType,
                    groupByResultType,
                });

            var linq = source.TryAddSpecificationQuery(specification);
            var linqGroupBy = (IQueryable<object>)groupByMethod
                .Invoke(obj: null, new object[]
                {
                    linq,
                    keySelectorExpression,
                    resultSelectorExpression,
                });

            return linqGroupBy.ToArrayAsync(cancellationToken);
        }
    }
}
