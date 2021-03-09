using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.FluentValidation
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> SetValidatorWithParentContext<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IValidator<TProperty> validator, params string[] ruleSets)
        {
            var adaptor = new ChildValidatorAdaptor<T, TProperty>(validator, validator.GetType())
            {
                RuleSets = ruleSets
            };
            SetPassThroughParentContext(adaptor, value: true);
            return ruleBuilder.SetValidator(adaptor);
        }

        public static IRuleBuilderOptions<T, TProperty> SetValidatorWithParentContext<T, TProperty, TValidator>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, TValidator> validatorProvider, params string[] ruleSets)
            where TValidator : IValidator<TProperty>
        {
            var adaptor = new ChildValidatorAdaptor<T, TProperty>(context => validatorProvider((T)context.InstanceToValidate), typeof(TValidator))
            {
                RuleSets = ruleSets
            };
            SetPassThroughParentContext(adaptor, value: true);
            return ruleBuilder.SetValidator(adaptor);
        }

        public static IRuleBuilderOptions<T, TProperty> SetValidatorWithParentContext<T, TProperty, TValidator>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, TProperty, TValidator> validatorProvider, params string[] ruleSets)
            where TValidator : IValidator<TProperty>
        {
            var adaptor = new ChildValidatorAdaptor<T, TProperty>(context => validatorProvider((T)context.InstanceToValidate, (TProperty)context.PropertyValue), typeof(TValidator))
            {
                RuleSets = ruleSets
            };
            SetPassThroughParentContext(adaptor, value: true);
            return ruleBuilder.SetValidator(adaptor);
        }

        private static void SetPassThroughParentContext<T, TProperty>(ChildValidatorAdaptor<T, TProperty> adaptor, bool value)
        {
            var property = typeof(ChildValidatorAdaptor<T, TProperty>)
                .GetProperty("PassThroughParentContext", BindingFlags.Instance | BindingFlags.NonPublic);
            property.SetValue(adaptor, value);
        }
    }
}
