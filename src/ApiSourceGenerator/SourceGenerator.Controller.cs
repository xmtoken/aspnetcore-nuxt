using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.ApiSourceGenerator
{
    public partial class SourceGenerator
    {
        public void AddControllerSource(GeneratorExecutionContext context, INamespaceSymbol featureNamespace, INamedTypeSymbol entityType, string[] excludePropertyNames, string[] primaryKeyPropertyNames)
        {
            var entityNameWithoutSufix = Regex.Replace(entityType.Name, "Entity$", string.Empty);

            var buildSourceNamespace = $"{featureNamespace.ToDisplayString()}.Controllers";
            var buildSourceClassName = $"{featureNamespace.Name}ControllerBase";

            var primaryKeyProperties = entityType
                .GetMembers()
                .OfType<IPropertySymbol>()
                .Where(x => primaryKeyPropertyNames.Contains(x.Name))
                .Select(x => new
                {
                    x.Name,
                    Type = x.Type.ToDisplayString().TrimEnd('?'),
                })
                .Select(x => new
                {
                    x.Name,
                    x.Type,
                    Constraint = x.Type switch
                    {
                        "int" => "int",
                        "bool" => "bool",
                        "DateTime" => "datetime",
                        "decimal" => "decimal",
                        "double" => "double",
                        "float" => "float",
                        "Guid" => "guid",
                        "long" => "long",
                        _ => null,
                    },
                })
                .ToArray();

            var primaryKeyRouteTemplates = primaryKeyProperties
                .Select(x => x.Constraint is null ? $"{{{x.Name.ToCamelCase()}}}" : $"{{{x.Name.ToCamelCase()}:{x.Constraint}}}");

            var primaryKeyRouteTemplate = string.Join("/", primaryKeyRouteTemplates);

            var primaryKeyRouteParameters = primaryKeyProperties
                .Select(x => $"[FromRoute] {x.Type} {x.Name.ToCamelCase()}");

            var primaryKeyRouteParameter = string.Join(", ", primaryKeyRouteParameters);

            var namespaces = Enumerable
                .Empty<string>()
                .Append($"FluentValidation")
                .Append($"FluentValidation.AspNetCore")
                .Append($"Microsoft.AspNetCore.Http")
                .Append($"Microsoft.AspNetCore.Mvc")
                .Append($"System")
                .Append($"System.Collections.Generic")
                .Append($"System.Linq")
                .Append($"System.Threading")
                .Append($"System.Threading.Tasks")
                .Append($"{entityType.ContainingNamespace.ToDisplayString()}")
                .Append($"{featureNamespace.ToDisplayString()}.Models")
                .Append($"{featureNamespace.ToDisplayString()}.Specifications")
                .Append($"{featureNamespace.ToDisplayString()}.Validators")
                .Append($"{nameof(AspNetCoreNuxt)}.Applications.WebHost.Core.Authorizations")
                .Append($"{nameof(AspNetCoreNuxt)}.Applications.WebHost.Core.Models")
                .Append($"{nameof(AspNetCoreNuxt)}.Applications.WebHost.Core.UseCases")
                .Append($"{nameof(AspNetCoreNuxt)}.Applications.WebHost.Core.Validators")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.AspNetCore.Mvc")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.AspNetCore.Mvc.ApplicationModels")
                .Select(x => $"using {x};")
                .OrderBy(x => x)
                .Distinct();

            var builder = new StringBuilder();
            builder.Append(string.Join(Environment.NewLine, namespaces));
            builder.Append($@"
namespace {buildSourceNamespace}
{{
    [ApiController]
    [Route(""api/$[controller]"")]
    public abstract partial class {buildSourceClassName}<TUseCase> : ControllerBase
        where TUseCase : UseCaseBase<{entityType.Name}>
    {{
        protected TUseCase {entityNameWithoutSufix}UseCase {{ get; }}
        protected IExpandoObjectFactory ExpandoObjectFactory {{ get; }}
        protected IValidatorFactory ValidatorFactory {{ get; }}
        protected {buildSourceClassName}(TUseCase useCase, IExpandoObjectFactory expandoObjectFactory, IValidatorFactory validatorFactory)
        {{
            {entityNameWithoutSufix}UseCase = useCase;
            ExpandoObjectFactory = expandoObjectFactory;
            ValidatorFactory = validatorFactory;
        }}
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<{entityType.Name}>))]
        public virtual async Task<object> Get({entityNameWithoutSufix}SearchConditions conditions, ISortQuery<{entityType.Name}> sort, IPagingQuery paging, IFieldQuery<{entityType.Name}> field, CancellationToken cancellationToken)
        {{
            var specification = new {entityNameWithoutSufix}SearchSpecification(conditions);
            var results = await {entityNameWithoutSufix}UseCase.GetAsync(specification, sort, paging, field, cancellationToken);
            var propertyNames = field.PropertyNames.ToArray();
            var models = new List<object>();
            foreach (var result in results)
            {{
                var model = await ExpandoObjectFactory.CreateAsync(
                    typeof({entityType.Name}),
                    result,
                    propertyNames,
                    Operations.Read,
                    includeUnauthorizedProperty: true,
                    includeNullObject: true);
                models.Add(model);
            }}
            return models;
        }}
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<{entityType.Name}>))]
        [Route(""aggregate"")]
        public virtual async Task<object> GetAggregate({entityNameWithoutSufix}SearchConditions conditions, IGroupQuery<{entityType.Name}> group, IAggregateQuery<{entityType.Name}> aggregate, CancellationToken cancellationToken)
        {{
            var specification = new {entityNameWithoutSufix}SearchSpecification(conditions);
            var results = await {entityNameWithoutSufix}UseCase.GetAggregateAsync(specification, group, aggregate, cancellationToken);
            var propertyNames = Enumerable.Concat(
                group.PropertyNames,
                aggregate.GetAggregations().Select(x => x.PropertyName)).ToArray();
            var models = new List<object>();
            foreach (var result in results)
            {{
                var model = await ExpandoObjectFactory.CreateAsync(
                    typeof({entityType.Name}),
                    result,
                    propertyNames,
                    Operations.Read,
                    includeUnauthorizedProperty: true,
                    includeNullObject: true);
                models.Add(model);
            }}
            return models;
        }}
        [HttpGet]
        [Route(""count"")]
        public virtual async Task<int> GetCount({entityNameWithoutSufix}SearchConditions conditions, IPagingQuery paging, CancellationToken cancellationToken)
        {{
            var specification = new {entityNameWithoutSufix}SearchSpecification(conditions);
            return await {entityNameWithoutSufix}UseCase.GetCountAsync(specification, paging, cancellationToken);
        }}");
            if (primaryKeyProperties.Length > 0)
            {
                builder.Append($@"
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof({entityType.Name}))]
        [Route(""{primaryKeyRouteTemplate}"")]
        public virtual async Task<object> Get({primaryKeyRouteParameter}, IFieldQuery<{entityType.Name}> field, CancellationToken cancellationToken)
        {{
            var keyValues = new object[] {{ {string.Join(", ", primaryKeyProperties.Select(x => x.Name.ToCamelCase()))} }};
            var result = await {entityNameWithoutSufix}UseCase.GetByPrimaryKeyAsync(keyValues, field, cancellationToken);
            var propertyNames = field.PropertyNames.ToArray();
            return await ExpandoObjectFactory.CreateAsync(
                typeof({entityType.Name}),
                result,
                propertyNames,
                Operations.Read,
                includeUnauthorizedProperty: true,
                includeNullObject: true);
        }}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof({entityType.Name}))]
        public virtual async Task<IActionResult> Post([FromBody] Api{entityType.Name} model)
        {{
            var validator = ValidatorFactory.GetValidator<Api{entityType.Name}>();
            async Task ValidateAsync()
            {{
                if (validator is not null)
                {{
                    var validationResult = await validator.ValidateAsync(model, options => options.IncludeRuleSets({entityNameWithoutSufix}ValidatorRuleSet.Add));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }}
            }};

            await ValidateAsync();
            if (ModelState.ErrorCount > 0)
            {{
                return ValidationProblem();
            }}

            var result = await {entityNameWithoutSufix}UseCase.AddAsync(model);
            if (result.Succeeded)
            {{
                return CreatedAtAction(
                    nameof(Get),
                    routeValues: new
                    {{");
                foreach (var primaryKeyProperty in primaryKeyProperties)
                {
                    builder.Append($@"
                        result.Entity.{primaryKeyProperty.Name},");
                }
                builder.Append($@"
                    }},
                    value: result.Entity);
            }}
            else
            {{
                await ValidateAsync();
                if (ModelState.IsValid)
                {{
                    throw new InvalidOperationException();
                }}
                return ValidationProblem();
            }}
        }}
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof({entityType.Name}))]
        [Route(""{primaryKeyRouteTemplate}"")]
        public virtual async Task<IActionResult> Patch({primaryKeyRouteParameter}, [FromBody] Api{entityType.Name} model)
        {{
            var validator = ValidatorFactory.GetValidator<Api{entityType.Name}>();
            async Task ValidateAsync()
            {{
                if (validator is not null)
                {{
                    var validationResult = await validator.ValidateAsync(model, options => options.IncludeRuleSets({entityNameWithoutSufix}ValidatorRuleSet.Update));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }}
            }};

            await ValidateAsync();
            if (ModelState.ErrorCount > 0)
            {{
                return ValidationProblem();
            }}

            var keyValues = new object[] {{ {string.Join(", ", primaryKeyProperties.Select(x => x.Name.ToCamelCase()))} }};
            var result = await {entityNameWithoutSufix}UseCase.UpdateAsync(keyValues, model);
            if (result.Succeeded)
            {{
                return Ok(value: result.Entity);
            }}
            else
            {{
                await ValidateAsync();
                if (ModelState.IsValid)
                {{
                    throw new InvalidOperationException();
                }}
                return ValidationProblem();
            }}
        }}
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof({entityType.Name}))]
        [Route(""{primaryKeyRouteTemplate}"")]
        public virtual async Task<IActionResult> Put({primaryKeyRouteParameter}, [FromBody] Api{entityType.Name} model)
        {{
            var validator = ValidatorFactory.GetValidator<Api{entityType.Name}>();
            async Task ValidateAsync()
            {{
                if (validator is not null)
                {{
                    var validationResult = await validator.ValidateAsync(model, options => options.IncludeRuleSets({entityNameWithoutSufix}ValidatorRuleSet.AddOrUpdate));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }}
            }};

            await ValidateAsync();
            if (ModelState.ErrorCount > 0)
            {{
                return ValidationProblem();
            }}

            var keyValues = new object[] {{ {string.Join(", ", primaryKeyProperties.Select(x => x.Name.ToCamelCase()))} }};
            var result = await {entityNameWithoutSufix}UseCase.AddOrUpdateAsync(keyValues, model);
            if (result.Succeeded)
            {{
                return Ok(value: result.Entity);
            }}
            else
            {{
                await ValidateAsync();
                if (ModelState.IsValid)
                {{
                    throw new InvalidOperationException();
                }}
                return ValidationProblem();
            }}
        }}
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<{entityType.Name}>))]
        public virtual async Task<IActionResult> Put([FromBody] Api{entityType.Name}[] models)
        {{
            var validator = ValidatorFactory.GetValidator<Api{entityType.Name}[]>();
            async Task ValidateAsync()
            {{
                if (validator is not null)
                {{
                    var validationResult = await validator.ValidateAsync(models, options => options.IncludeRuleSets({entityNameWithoutSufix}ValidatorRuleSet.AddOrUpdateOnBulk));
                    validationResult.AddToModelState(ModelState, prefix: null);
                }}
            }};

            await ValidateAsync();
            if (ModelState.ErrorCount > 0)
            {{
                return ValidationProblem();
            }}

            var values = models.Select(model => (
                KeyValues: new object[] {{ {string.Join(", ", primaryKeyProperties.Select(x => "model." + x.Name))} }},
                Model: ({entityType.Name})model)).ToArray();
            var result = await {entityNameWithoutSufix}UseCase.AddOrUpdateRangeAsync(values);
            if (result.Succeeded)
            {{
                return Ok(value: result.Entities);
            }}
            else
            {{
                await ValidateAsync();
                if (ModelState.IsValid)
                {{
                    throw new InvalidOperationException();
                }}
                return ValidationProblem();
            }}
        }}
        [HttpDelete]
        [Route(""{primaryKeyRouteTemplate}"")]
        public virtual async Task Delete({primaryKeyRouteParameter})
        {{
            var keyValues = new object[] {{ {string.Join(", ", primaryKeyProperties.Select(x => x.Name.ToCamelCase()))} }};
            await {entityNameWithoutSufix}UseCase.RemoveAsync(keyValues);
        }}
        [HttpDelete]
        public virtual async Task Delete([CustomizeValidator(RuleSet = {entityNameWithoutSufix}ValidatorRuleSet.DeleteOnBulk)][FromBody] Api{entityType.Name}[] models)
        {{
            var keyValuesCollection = models.Select(model => new object[] {{ {string.Join(", ", primaryKeyProperties.Select(x => "model." + x.Name))} }} ).ToArray();
            await {entityNameWithoutSufix}UseCase.RemoveRangeAsync(keyValuesCollection);
        }}");
            }
            builder.Append($@"
    }}
    public abstract partial class {buildSourceClassName} : {buildSourceClassName}<UseCaseBase<{entityType.Name}>>
    {{
        protected {buildSourceClassName}(UseCaseBase<{entityType.Name}> useCase, IExpandoObjectFactory expandoObjectFactory, IValidatorFactory validatorFactory)
            : base(useCase, expandoObjectFactory, validatorFactory)
        {{
        }}
    }}
}}");

            context.AddSource($"{Guid.NewGuid()}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
