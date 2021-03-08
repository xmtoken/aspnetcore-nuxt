using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.FluentValidation
{
    /// <summary>
    /// 
    /// </summary>
    public static class CamelCaseNaming
    {
        /// <summary>
        /// 
        /// </summary>
        public static Func<Type, MemberInfo, LambdaExpression, string> PropertyNameResolver { get; } = (_, member, expression) =>
        {
            if (expression is not null)
            {
                var properties = PropertyChain.FromExpression(expression);
                if (properties.Count > 0)
                {
                    var separator = ValidatorOptions.Global.PropertyChainSeparator;
                    var propertyNames = properties.ToString().Split(separator);
                    return string.Join(separator, propertyNames.Select(property => property.ToCamelCase()));
                }
            }
            return member?.Name;
        };
    }
}
