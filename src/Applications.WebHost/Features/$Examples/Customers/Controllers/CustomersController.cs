using AspNetCoreNuxt.ApiSourceGenerator;
using AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.UseCases;
using AspNetCoreNuxt.Domains.Entities._Examples;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using NJsonSchema.Annotations;
using System.Collections.Generic;

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
            //Validators.CustomerValidatorRuleSet
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
