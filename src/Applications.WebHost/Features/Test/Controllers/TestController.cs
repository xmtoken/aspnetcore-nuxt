using AspNetCoreNuxt.Applications.WebHost.Core.Validators;
using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Models;
using AspNetCoreNuxt.Extensions.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
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
                RuleSet(ruleSetName: RuleSetName.Add, () =>
                {
                    RuleFor(x => x.Text)
                        .NotNull();
                });
            }

            protected override bool PreValidate(ValidationContext<ApiManyEntity> context, ValidationResult result)
            {
                return base.PreValidate(context, result);
            }
        }

        public class OneEntityValidator : AbstractValidator<ApiOneEntity>
        {
            private ApiCustomerEntity Parent;

            public OneEntityValidator()
            {
                RuleSet(ruleSetName: RuleSetName.Add, () =>
                {
                    RuleFor(x => x.Text)
                        .NotNull()
                        .Custom((model, validationContext) =>
                        {
                            var context = validationContext;
                        });
                });
            }

            protected override bool PreValidate(ValidationContext<ApiOneEntity> context, ValidationResult result)
            {
                Parent = context.GetParentContext<ApiCustomerEntity>().InstanceToValidate;
                return base.PreValidate(context, result);
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
                    .SetValidatorWithParentContext(model =>
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
                    .SetValidatorWithParentContext(model => factory.GetValidator<ApiOneEntity>());
                // or
                //RuleFor(x => x.One)
                //    .NotNull()
                //    .SetValidator((root, model) =>
                //    {
                //        var validator = new InlineValidator<ApiOneEntity>();
                //        validator.RuleSet(ruleSetName: RuleSetName.Add, () =>
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
                    .SetValidatorWithParentContext(model => factory.GetValidator<ApiManyEntity>());
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
                //                validator.RuleSet(ruleSetName: RuleSetName.Add, () =>
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
                context.GetParentContext();
                return base.PreValidate(context, result);
            }
        }

        public class CustomerEntityCollectionValidator : AbstractValidator<ApiCustomerEntity[]>
        {
            public CustomerEntityCollectionValidator(IValidatorFactory factory)
            {
                RuleForEach(x => x)
                    .RemoveForEachPropertyName()
                    .ConfigureIdentifiedIndexBuilder()
                    .SetValidatorWithParentContext((models, model) =>
                    {
                        var ruleSets = model.Id.HasValue ? RuleSetName.Update : RuleSetName.Add;

                        var validator = new InlineValidator<ApiCustomerEntity>();
                        validator
                            .RuleFor(x => x)
                            .SetValidatorWithParentContext(factory.GetValidator<ApiCustomerEntity>(), ruleSets, "default");

                        validator
                            .RuleFor(x => x.Id)
                            .Must(_ => models.Count(x => x.Id == model.Id) == 1).WithMessage("Duplicate");
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

        private readonly IValidatorFactory ValidatorFactory;
        public TestController(IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApiCustomerEntity model)
        {
            var validator = ValidatorFactory.GetValidator<ApiCustomerEntity>();
            async Task ValidateAsync()
            {
                if (validator is not null)
                {
                    var validationResult = await validator.ValidateAsync(model, options => options.IncludeRuleSets(RuleSetName.Add));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }
            };

            await ValidateAsync();
            return ValidationProblem();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] ApiCustomerEntity model)
        {
            var validator = ValidatorFactory.GetValidator<ApiCustomerEntity>();
            async Task ValidateAsync()
            {
                if (validator is not null)
                {
                    var validationResult = await validator.ValidateAsync(model, options => options.IncludeRuleSets(RuleSetName.Update));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }
            };

            await ValidateAsync();
            return ValidationProblem();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ApiCustomerEntity[] model)
        {
            var validator = ValidatorFactory.GetValidator<ApiCustomerEntity[]>();
            async Task ValidateAsync()
            {
                if (validator is not null)
                {
                    var validationResult = await validator.ValidateAsync(model, options => options.IncludeRuleSets(RuleSetName.AddOrUpdateOnBulk));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }
            };

            await ValidateAsync();
            return ValidationProblem();
        }
    }
}
