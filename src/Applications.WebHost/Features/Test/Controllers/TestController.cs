using AspNetCoreNuxt.Applications.WebHost.Core.Validators;
using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Models;
using AspNetCoreNuxt.Extensions.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using RuleSetName = AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Validators.CustomerValidatorRuleSet;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public class ManyEntityValidator : AbstractValidator<ApiManyEntity>
        {
            public ManyEntityValidator()
            {
                RuleSet(ruleSetName: "Add", () =>
                {
                    RuleFor(x => x.Text)
                        .NotNull();
                });
            }
        }

        public class OneEntityValidator : AbstractValidator<ApiOneEntity>
        {
            public OneEntityValidator()
            {
                RuleSet(ruleSetName: "Add", () =>
                {
                    RuleFor(x => x.Text)
                        .NotNull();
                });
            }
        }

        public class CustomerEntityValidator : AbstractValidator<ApiCustomerEntity>
        {
            public CustomerEntityValidator(IValidatorFactory factory)
            {
                //RuleFor(x => x.Text)
                //    .NotNull();
                RuleFor(x => x.Text)
                    .NotNull()
                    .SetValidator(model =>
                    {
                        // Textがnullのときはcallされない
                        var validator = new InlineValidator<string>();
                        validator
                            .RuleFor(x => x)
                            .MinimumLength(10);
                        return validator;
                    });

                RuleFor(x => x.One)
                    .NotNull()
                    .SetValidator(model => factory.GetValidator<ApiOneEntity>());
                // or
                //RuleFor(x => x.One)
                //    .NotNull()
                //    .SetValidator((root, model) =>
                //    {
                //        var validator = new InlineValidator<ApiOneEntity>();
                //        validator.RuleSet(ruleSetName: "Add", () =>
                //        {
                //            validator
                //                .RuleFor(x => x.Text)
                //                .NotNull();
                //        });
                //        return validator;
                //    });

                RuleForEach(x => x.Many)
                    .ConfigureIdentifiedIndexBuilder()
                    .NotNull()
                    .SetValidator(model => factory.GetValidator<ApiManyEntity>());
                // or
                //RuleFor(x => x.Many)
                //    .NotNull()
                //    .SetValidator(root =>
                //    {
                //        var validator = new InlineValidator<ApiManyEntity[]>();
                //        validator
                //            .RuleForEach(x => x)
                //            .ConfigureIdentifiedIndexBuilder()
                //            .SetValidator((models, model) =>
                //            {
                //                var validator = new InlineValidator<ApiManyEntity>();
                //                validator.RuleSet(ruleSetName: "Add", () =>
                //                {
                //                    validator
                //                        .RuleFor(x => x.Text)
                //                        .NotNull();
                //                });
                //                return validator;
                //            });
                //        return validator;
                //    });
            }

            protected override bool PreValidate(ValidationContext<ApiCustomerEntity> context, ValidationResult result)
            {
                return base.PreValidate(context, result);
            }
        }

        public class CustomerEntityCollectionValidator : AbstractValidator<ApiCustomerEntity[]>
        {
            public CustomerEntityCollectionValidator(IValidatorFactory factory)
            {
                factory.GetValidator<ApiCustomerEntity>().ValidateAsync(instance: null, options: strategy =>
                 {
                     strategy.IncludeRuleSets("");
                 });
                //IHttpContextAccessor httpContextAccessor;

                RuleForEach(x => x)
                    .RemoveForEachPropertyName()
                    .ConfigureIdentifiedIndexBuilder()
                    .SetValidator((models, model) =>
                    {
                        var validator = new InlineValidator<ApiCustomerEntity>();
                        validator
                            .RuleFor(x => x)
                            .SetValidator(factory.GetValidator<ApiCustomerEntity>());

                        validator
                            .RuleFor(x => x.Id)
                            .Must(_ => models.All(x => x.Id != model.Id)).WithMessage("Duplicate");
                        return validator;
                    });

                RuleFor(x => x)
                    .Custom((models, validationContext) =>
                    {
                        validationContext.AddFailure("GlobalError");
                    });
            }

            protected override bool PreValidate(ValidationContext<ApiCustomerEntity[]> context, ValidationResult result)
            {
                return base.PreValidate(context, result);
            }
        }

        [HttpPost]
        public async Task<object> Post([CustomizeValidator(RuleSet = RuleSetName.Add)][FromBody] ApiCustomerEntity model)
        {
            await Task.CompletedTask;
            return model;
        }

        [HttpPut]
        public async Task<object> Put([CustomizeValidator(RuleSet = RuleSetName.AddOrUpdateOnBulk)][FromBody] ApiCustomerEntity[] model)
        {
            await Task.CompletedTask;
            return model;
        }
    }
}
