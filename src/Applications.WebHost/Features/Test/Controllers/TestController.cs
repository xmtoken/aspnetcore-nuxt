using AspNetCoreNuxt.Applications.WebHost.Core.Validators;
using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Models;
using AspNetCoreNuxt.Extensions.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
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
                RuleFor(x => x.Text)
                    .NotNull();
            }
        }

        public class OneEntityValidator : AbstractValidator<ApiOneEntity>
        {
            public OneEntityValidator()
            {
                RuleFor(x => x.Text)
                    .NotNull();
            }
        }

        public class CustomerEntityValidator : AbstractValidator<ApiCustomerEntity>
        {
            public CustomerEntityValidator(IValidatorFactory factory)
            {
                RuleFor(x => x.Text)
                    .NotNull();

                RuleFor(x => x.One)
                    .SetValidator(model => factory.GetValidator<ApiOneEntity>())
                    .NotNull();
                // or
                //RuleFor(x => x.One)
                //    .NotNull()
                //    .DependentRules(() =>
                //    {
                //        RuleFor(x => x.One.Text)
                //            .NotNull();
                //    });

                RuleForEach(x => x.Many)
                    .ConfigureIdentifiedIndexBuilder()
                    .SetValidator(model => factory.GetValidator<ApiManyEntity>())
                    .NotNull();
                // or
                //RuleFor(x => x.Many)
                //    .NotNull()
                //    .DependentRules(() =>
                //    {
                //        RuleForEach(x => x.Many)
                //            .ConfigureIdentifiedIndexBuilder()
                //            .SetValidator((_, model) =>
                //            {
                //                var validator = new InlineValidator<ApiManyEntity>();
                //                validator.RuleFor(x => x.Text)
                //                    .NotNull();
                //                return validator;
                //            });
                //    });
            }
        }

        public class CustomerEntityCollectionValidator : AbstractValidator<ApiCustomerEntity[]>
        {
            public CustomerEntityCollectionValidator(IValidatorFactory factory)
            {
                RuleForEach(x => x)
                    .RemoveForEachPropertyName()
                    .ConfigureIdentifiedIndexBuilder()
                    .SetValidator((_, model) =>
                    {
                        var validator = new InlineValidator<ApiCustomerEntity>();
                        validator
                            .RuleFor(x => x)
                            .SetValidator(factory.GetValidator<ApiCustomerEntity>());
                        return validator;
                    });

                RuleFor(x => x)
                    .Custom((models, validationContext) =>
                    {
                        validationContext.AddFailure("GlobalError");
                    });
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
