using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Models;
using FluentValidation;
using FluentValidation.Results;
using RuleSetName = AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Validators.CustomerValidatorRuleSet;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Validators
{
    public class IssueCollectionValidator : AbstractValidator<ApiCustomerEntity[]>
    {
        public IssueCollectionValidator()
        {
            RuleSet(ruleSetName: RuleSetName.AddOrUpdateOnBulk, () =>
            {
                //RuleForEach(x => x)
                //    .Hoge()
                //    .Configure(x => x.IndexBuilder = (_, models, model, index) => $"[{model.Identifier}]")
                //    .SetValidator((_, model) =>
                //    {
                //        var ruleSets = model.Id.HasValue ? RuleSetName.Update : RuleSetName.Add;
                //        var validator = new InlineValidator<CustomerEntity>();
                //        validator
                //            .RuleFor(x => x)
                //            .SetValidator(factory.GetValidator<CustomerEntity>(), ruleSets);
                //        return validator;
                //    });
            });

            RuleSet(ruleSetName: RuleSetName.DeleteOnBulk, () =>
            {
                //RuleForEach(x => x)
                //    .Hoge()
                //    .Configure(x => x.IndexBuilder = (_, models, model, index) => $"[{model.Identifier}]")
                //    .SetValidator((_, model) =>
                //    {
                //        var validator = new InlineValidator<CustomerEntity>();
                //        validator
                //            .RuleFor(x => x)
                //            .SetValidator(factory.GetValidator<CustomerEntity>());
                //        return validator;
                //    });
            });
        }

        protected override bool PreValidate(ValidationContext<ApiCustomerEntity[]> context, ValidationResult result)
        {
            return base.PreValidate(context, result);
        }
    }
}
