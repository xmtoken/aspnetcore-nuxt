using AspNetCoreNuxt.ApiSourceGenerator;
using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Models;
using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.UseCases;
using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Validators;
using AspNetCoreNuxt.Domains.Entities._Examples;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NJsonSchema.Converters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Controllers
{
    [AutoGenerateApi(
        typeof(CustomerEntity),
        PrimaryKey = new[] { nameof(CustomerEntity.Id) },
        Exclude = new string[] { })]
    public partial class CustomersController : CustomersControllerBase<CustomerUseCase>
    {
        public CustomersController(CustomerUseCase useCase, IExpandoObjectFactory expandoObjectFactory, IValidatorFactory validatorFactory)
            : base(useCase, expandoObjectFactory, validatorFactory)
        {
            IValidatorInterceptor ggg;
            Authorizations.DefaultCustomerAuthorizationHandler xxa;
            Models.CustomerSearchConditions xx;
            Models.ApiCustomerEntity x1x;
            Models.ApiOneEntity x2x;
            Models.ApiManyEntity x3x;
            Specifications.CustomerSearchSpecification xxm;
            //Validators.CustomerValidatorRuleSet set;
        }

        public override async Task<IActionResult> Post([CustomizeValidator(RuleSet = "Add"), FromBody] ApiCustomerEntity model)
        {
            var validator = ValidatorFactory.GetValidator<ApiCustomerEntity>();
            async Task ValidateAsync()
            {
                if (validator is not null)
                {
                    var validationResult = await validator.ValidateAsync(model, options => options.IncludeRuleSets(CustomerValidatorRuleSet.Add));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }
            };

            await ValidateAsync();
            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }

            var result = await CustomerUseCase.AddAsync(model);
            if (result.Succeeded)
            {
                return CreatedAtAction(
                    nameof(Get),
                    routeValues: new
                    {
                        result.Entity.Id,
                    },
                    value: result.Entity);
            }
            else
            {
                await ValidateAsync();
                if (ModelState.IsValid)
                {
                    throw new InvalidOperationException();
                }
                return ValidationProblem();
            }
        }
    }
}

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Models
{
    //public partial class CustomerSearchConditions
    //{
    //    [JsonSchemaType(typeof(IEnumerable<string>))]
    //    public SearchConditions<int> Xxx { get; set; }
    //    public IEnumerable<string> arrayprop { get; set; }
    //}
}
