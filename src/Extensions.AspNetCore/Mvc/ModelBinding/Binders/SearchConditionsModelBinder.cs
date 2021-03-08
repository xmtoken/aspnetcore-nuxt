using AspNetCoreNuxt.Extensions.AspNetCore.Logging;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="SearchConditions{T}"/> クラスのモデルバインダーを表します。
    /// </summary>
    /// <typeparam name="T">バインド先の <see cref="SearchConditions{T}"/> オブジェクトの値の型。</typeparam>
    public class SearchConditionsModelBinder<T> : IModelBinder
    {
        /// <summary>
        /// <see cref="ILogger"/> オブジェクトを表します。
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// <see cref="SearchConditions{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> オブジェクト。</param>
        public SearchConditionsModelBinder(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<SearchConditionsModelBinder<T>>();
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult.Length == 0)
            {
                return Task.CompletedTask;
            }

            var includeInvalidPredicate = false;
            try
            {
                var normalizedValues = valueProviderResult.Values.Select(value
                    => value.StartsWith("(", StringComparison.Ordinal)
                    && value.StartsWith(")", StringComparison.Ordinal) ? value : $"({value})");

                // extract  '(==text)|(==text)|(==text)'
                //      to ['(==text)', '|', '(==text), '|', '(==text)']
                var values = Regex.Split(string.Join("|", normalizedValues), @"(?<=[^\\]\))(?=.)|(?<=[^\\]\|)(?=.)|(?<=[^\\]&)(?=.)");

                // extract ['(==text)', '|', '(==text), '|', '(==text)']
                //      to ['==text', '==text, '==text']
                var comparisonPredicates = values
                    .Where((_, i) => i % 2 == 0)
                    .Select(x => Regex.Replace(x, @"(^\()|(\)$)", string.Empty))
                    .ToArray();

                var comparisons = comparisonPredicates
                    .Select(x => ConvertToSearchConditionsComparisonOrNull(x))
                    .Where(x => x is not null)
                    .ToArray();

                if (comparisonPredicates.Length != comparisons.Length)
                {
                    includeInvalidPredicate = true;
                }

                if (comparisons.Length == 0)
                {
                    return Task.CompletedTask;
                }

                // extract ['(==text)', '|', '(==text), '|', '(==text)']
                //      to ['|', '|']
                var logicalOperatorPredicates = values
                    .Where((_, i) => i % 2 != 0)
                    .ToArray();

                var logicalOperator = ConvertToLogicalOperatorOrNull(logicalOperatorPredicates);

                if (logicalOperator is null)
                {
                    includeInvalidPredicate = true;
                    logicalOperator = LogicalOperator.OrElse;
                }

                var model = new SearchConditions<T>(valueProviderResult.Values, bindingContext.ModelName, logicalOperator.Value, comparisons);
                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }
            finally
            {
                if (includeInvalidPredicate)
                {
                    Logger.IncludeInvalidSearchConditionsPredicate(valueProviderResult.Values);
                }
            }
        }

        /// <summary>
        /// 指定された述語を <see cref="SearchConditionsComparison{T}"/> オブジェクトへ変換します。
        /// </summary>
        /// <param name="predicate">検索条件を表す述語。</param>
        /// <returns>述語を変換できた場合は <see cref="SearchConditionsComparison{T}"/> オブジェクト。それ以外の場合は null。</returns>
        private static SearchConditionsComparison<T> ConvertToSearchConditionsComparisonOrNull(string predicate)
        {
            var text = predicate;

            if (text == "$null")
            {
                return new SearchConditionsComparison<T>(ComparisonOperator.IsNull, value: default);
            }

            var comparisonOperator = default(ComparisonOperator);

            if (text.StartsWith("==", StringComparison.Ordinal))
            {
                text = text[2..];
                if (text.StartsWith("*", StringComparison.Ordinal) &&
                    text.EndsWith("*", StringComparison.Ordinal) &&
                    text.EndsWith(@"\*", StringComparison.Ordinal) == false)
                {
                    if (typeof(T) != typeof(string))
                    {
                        return null;
                    }
                    text = text[1..^1];
                    comparisonOperator = ComparisonOperator.Contains;
                }
                else if (text.StartsWith("*", StringComparison.Ordinal))
                {
                    if (typeof(T) != typeof(string))
                    {
                        return null;
                    }
                    text = text[1..];
                    comparisonOperator = ComparisonOperator.StartsWith;
                }
                else if (text.EndsWith("*", StringComparison.Ordinal) &&
                         text.EndsWith(@"\*", StringComparison.Ordinal) == false)
                {
                    if (typeof(T) != typeof(string))
                    {
                        return null;
                    }
                    text = text[..^1];
                    comparisonOperator = ComparisonOperator.EndsWith;
                }
                else
                {
                    comparisonOperator = ComparisonOperator.Equals;
                }
            }
            else if (text.StartsWith("!=", StringComparison.Ordinal))
            {
                text = text[2..];
                if (text.StartsWith("*", StringComparison.Ordinal) &&
                    text.EndsWith("*", StringComparison.Ordinal) &&
                    text.EndsWith(@"\*", StringComparison.Ordinal) == false)
                {
                    if (typeof(T) != typeof(string))
                    {
                        return null;
                    }
                    text = text[1..^1];
                    comparisonOperator = ComparisonOperator.NotContains;
                }
                else if (text.StartsWith("*", StringComparison.Ordinal))
                {
                    if (typeof(T) != typeof(string))
                    {
                        return null;
                    }
                    text = text[1..];
                    comparisonOperator = ComparisonOperator.NotStartsWith;
                }
                else if (text.EndsWith("*", StringComparison.Ordinal) &&
                         text.EndsWith(@"\*", StringComparison.Ordinal) == false)
                {
                    if (typeof(T) != typeof(string))
                    {
                        return null;
                    }
                    text = text[..^1];
                    comparisonOperator = ComparisonOperator.NotEndsWith;
                }
                else
                {
                    comparisonOperator = ComparisonOperator.NotEquals;
                }
            }
            else if (text.StartsWith(">=", StringComparison.Ordinal))
            {
                text = text[2..];
                comparisonOperator = ComparisonOperator.GreaterThanOrEquals;
            }
            else if (text.StartsWith(">", StringComparison.Ordinal))
            {
                text = text[1..];
                comparisonOperator = ComparisonOperator.GreaterThan;
            }
            else if (text.StartsWith("<=", StringComparison.Ordinal))
            {
                text = text[2..];
                comparisonOperator = ComparisonOperator.LessThanOrEquals;
            }
            else if (text.StartsWith("<", StringComparison.Ordinal))
            {
                text = text[1..];
                comparisonOperator = ComparisonOperator.LessThan;
            }
            else
            {
                return null;
            }

            var unescaped = Unescape(text);
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter.IsValid(unescaped) &&
                converter.ConvertFrom(unescaped) is T value &&
                (value is not TimeSpan timespan || (TimeSpan.Zero <= timespan && timespan < TimeSpan.FromDays(1))))
            {
                return new SearchConditionsComparison<T>(comparisonOperator, value);
            }
            else
            {
                return null;
            }

            static string Unescape(string value)
            {
                var charactors = new string[] { "!", @"\$", "&", @"\(", @"\)", @"\*", "<", "=", ">", @"\|" };
                return Regex.Replace(value, @$"\\({string.Join("|", charactors)})", "$1");
            }
        }

        /// <summary>
        /// 指定された述語を論理演算子を表す <see cref="LogicalOperator"/> 値へ変換します。
        /// </summary>
        /// <param name="predicates">論理演算子を表す述語。</param>
        /// <returns>述語を変換できた場合は論理演算子を表す <see cref="LogicalOperator"/> 値。それ以外の場合は null。</returns>
        private static LogicalOperator? ConvertToLogicalOperatorOrNull(string[] predicates)
        {
            if (predicates.Length == 0)
            {
                return null;
            }
            switch (predicates[0])
            {
                case "&":
                    if (predicates.All(x => x == predicates[0]))
                    {
                        return LogicalOperator.AndAlso;
                    }
                    else
                    {
                        return null;
                    }
                case "|":
                    if (predicates.All(x => x == predicates[0]))
                    {
                        return LogicalOperator.OrElse;
                    }
                    else
                    {
                        return null;
                    }
                default:
                    return null;
            }
        }
    }
}
