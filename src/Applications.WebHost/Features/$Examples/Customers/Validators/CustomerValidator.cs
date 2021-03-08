using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Models;
using FluentValidation;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Validators
{
    public class IssueValidator : AbstractValidator<ApiCustomerEntity>
    {
        public IssueValidator()
        {
            RuleFor(x => x.Id).IsInEnum();

            //RuleSet(ruleSetName: $"{RuleSetName.DeleteOnBulk}", () =>
            //{
            //    RuleFor(x => x.Id)
            //        .NotNull()
            //        .Must((model, value, validationContext) =>
            //        {
            //            return true;
            //        });
            //});

            //RuleSet(ruleSetName: $"{RuleSetName.Add},{RuleSetName.AddOrUpdate},{RuleSetName.Update}", () =>
            //{
            //    RuleFor(x => x.Text)
            //        .NotNull()
            //        .MaximumLength;
            //});

            //RuleSet(ruleSetName: $"{RuleSetName.Add},{RuleSetName.AddOrUpdate},{RuleSetName.Update}", () =>
            //{
            //    RuleFor(x => x.Description)
            //        .NotNull();
            //});
        }
    }
}
