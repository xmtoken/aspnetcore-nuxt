# Source Generator による API の自動実装

`RootNamespace.ApiSourceGenerator` 名前空間の `AutoGenerateApiAttribute` 属性を `Controller` クラスに付与することで API の実装コードを自動生成することができます。

## 自動生成されるコード

以下の `UserEntity` クラスを例に自動生成されるコードについて説明します。

```cs {.line-numbers}
namespace RootNamespace.Domains.Entities
{
    public class UserEntity
    {
        public int Id { get; set; } /* Primary Key */
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
```

`UserEntity` に関する API を公開するための `UsersController` クラスを作成し `AutoGenerateApiAttribute` 属性を付与します。
`AutoGenerateApiAttribute` 属性の引数は API で公開するリソースとなるエンティティの型を指定します。
ここでは `UserEntity` クラスをリソースとして公開する API を作成するため `typeof(UserEntity)` を指定します。
また、エンティティを一意に特定できる主キーが存在する場合は `PrimaryKey` プロパティに `UserEntity` クラスの主キーを構成するプロパティを列挙します。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    [AutoGenerateApi(typeof(UserEntity), PrimaryKey = new[] { nameof(UserEntity.Id) })]
    public class UsersController
    {
    }
}
```

### Authorization クラス

Source Generator によって、`ExpandObjectAuthorizationHandler<TRequirement, TResource>` クラスを継承した、以下のような `ExpandObjectAuthorizationHandler` クラスが自動生成されます。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Authorizations
{
    public sealed class DefaultUserAuthorizationHandler : ExpandObjectAuthorizationHandler<OperationAuthorizationRequirement, UserEntity>, IDependencyInjectionService<IAuthorizationHandler>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ExpandObjectAuthorizationResource<UserEntity> resource)
        {
            /* デフォルトの承認ハンドラーとして更新時の主キーの編集を禁止します。 */
            if (requirement.Name == Operations.Update.Name)
            {
                if (resource.PropertyName == nameof(UserEntity.Id))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
            }
            if (!context.HasFailed && !context.HasSucceeded)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
```

### Controller クラス

Source Generator によって `Controller` のベースクラスが `CustomersControllerBase` という名前で自動生成されます。
自動生成される `Controller` のベースクラスは以下のアクションを公開します。

+ HTTP GET `/api/users`
+ HTTP GET `/api/users/count`

`PrimaryKey` プロパティを指定した場合は以下のアクションも公開されます。

+ HTTP GET `/api/users/{id}`
+ HTTP POST `/api/users`
+ HTTP PATCH `/api/users/{id}`
+ HTTP PUT `/api/users`
+ HTTP PUT `/api/users/{id}`
+ HTTP DELETE `/api/users`
+ HTTP DELETE `/api/users/{id}`

自動生成された `Controller` クラスは継承して利用します。

```cs {.line-numbers highlight=4}
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    [AutoGenerateApi(typeof(UserEntity), PrimaryKey = new[] { nameof(UserEntity.Id) })]
    public class UsersController : UsersControllerBase
    {
        public UsersController(UserUseCaseBase useCase, IExpandoObjectFactory expandoObjectFactory, IValidatorFactory validatorFactory)
            : base(useCase, expandoObjectFactory, validatorFactory)
        {
        }
    }
}
```

自動生成されたアクションの一部を公開したくないケースではアクションメソッドをオーバーライドし `NonActionAttribute` をメソッドに付与します。
例えば自動生成される DELETE アクションを非公開としたい場合は以下の様になります

```cs {.line-numbers highlight=11-17}
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    [AutoGenerateApi(typeof(UserEntity), PrimaryKey = new[] { nameof(UserEntity.Id) })]
    public class UsersController : UsersControllerBase
    {
        public UsersController(UserUseCaseBase useCase, IExpandoObjectFactory expandoObjectFactory, IValidatorFactory validatorFactory)
            : base(useCase, expandoObjectFactory, validatorFactory)
        {
        }

        [NonAction]
        public override Task Delete([FromRoute] int id)
            => base.Delete(id);

        [NonAction]
        public override Task Delete([CustomizeValidator(RuleSet = UserValidatorRuleSet.DeleteOnBulk), FromBody] IdentifiedCustomerEntity[] models)
            => base.Delete(models);
    }
}
```

また、承認要件をアクションに付加したい場合も同様にアクションメソッドをオーバーライドします。

```cs {.line-numbers highlight=11-17}
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    [AutoGenerateApi(typeof(UserEntity), PrimaryKey = new[] { nameof(UserEntity.Id) })]
    public class UsersController : UsersControllerBase
    {
        public UsersController(UserUseCaseBase useCase, IExpandoObjectFactory expandoObjectFactory, IValidatorFactory validatorFactory)
            : base(useCase, expandoObjectFactory, validatorFactory)
        {
        }

        [Authorize]
        public override Task Delete([FromRoute] int id)
            => base.Delete(id);

        [Authorize]
        public override Task Delete([CustomizeValidator(RuleSet = UserValidatorRuleSet.DeleteOnBulk), FromBody] IdentifiedCustomerEntity[] models)
            => base.Delete(models);
    }
}
```

### Model クラス

Source Generator によって、`RootNamespace.Applications.WebHost.Core.Models` 名前空間の `IIdentification` インターフェイスを実装した、以下のような `IdentifiedEntity` クラスが自動生成されます。
以下のクラスは一括登録、一括更新、一括削除の一括処理でエンティティの識別子を含むために定義されます。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Models
{
    public partial class IdentifiedUserEntity : UserEntity, IIdentification
    {
        public int? Identifier { get; set; }
    }
}
```

### SearchConditions クラス

Source Generator によって、リソースとして指定したエンティティのすべてのプロパティをメンバーとした、以下のような `SearchConditions` クラスが自動生成されます。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Models
{
    public partial class UserSearchConditions
    {
        public SearchConditions<int> Id { get; set; }
        public SearchConditions<string> UserName { get; set; }
        public SearchConditions<string> Password { get; set; }
    }
}
```

しかし `UserEntity` クラスの `Password` のように公開することが望ましくないプロパティがあります。
そのようなプロパティは `AutoGenerateApiAttribute` 属性の `Exclude` プロパティに除外するプロパティとして列挙します。

```cs {.line-numbers highlight=3}
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    [AutoGenerateApi(typeof(UserEntity), PrimaryKey = new[] { nameof(UserEntity.Id) }, Exclude = new[] { nameof(UserEntity.Password) })]
    public class UsersController : UsersControllerBase
    {
    }
}
```

`Exclude` プロパティとして列挙することで `SearchConditions` クラスのメンバーから除外することができます。
上記のように `Exclude` プロパティとして `Password` プロパティを指定した場合に自動生成される `SearchConditions` クラスは以下のようになります。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Models
{
    public partial class UserSearchConditions
    {
        public SearchConditions<int> Id { get; set; }
        public SearchConditions<string> UserName { get; set; }
    }
}
```

`SearchConditions` クラスは `partial class` として定義されるため、追加で検索条件を定義することができます。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Models
{
    public partial class UserSearchConditions
    {
        // Id の先頭が 1 で始まるユーザーは管理ユーザーを表し管理ユーザーのみを抽出する条件を追加で公開する。
        public SearchConditions<bool> OnlySpecialUser { get; set; }
    }
}
```

### SearchSpecification クラス

Source Generator によって自動生成された `SearchConditions` クラスのプロパティでのクエリ構築処理を含んだ、以下のような `SearchSpecification` クラスが自動生成されます。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Specifications
{
    public partial class UserSearchSpecification : SearchSpecification<UserEntity>
    {
        public UserSearchSpecification(UserSearchConditions conditions)
        {
            Add(conditions.Id);
            Add(conditions.UserName);
        }
        partial void BuildExpression();
    }
}
```

`SearchSpecification` クラスは `partial class` として定義されるため、`SeachConditions` に手動で追加した検索条件は手動で絞り込み処理を記述します。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Specifications
{
    public partial class UserSearchSpecification
    {
        partial void BuildExpression()
        {
            if (conditions.OnlySpecialUser)
            {
                Add(x => x.Id.ToString().StartsWith("1"));
            }
        }
    }
}
```

### UseCase クラス

Source Generator によって、`UseCaseBase<T>` クラスを継承した、以下のような `UseCase` クラスが自動生成されます。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.UseCases
{
    public partial class UserUseCaseBase : UseCaseBase<UserEntity>, IDependencyInjectionService
    {
        public UserUseCaseBase(AppDbContext context, IExpandoObjectFactory expandoObjectFactory, IMapper mapper)
            : base(context, expandoObjectFactory, mapper)
        {
        }
    }
}
```

`UseCase` クラスの処理を拡張する必要がある場合は、以下のように自動生成された `UseCase` クラスを継承して新しい `UseCase` クラスを作成します。
また、コントローラーの定義も新しく作成した `UseCase` クラスに対応するようジェネリックパラメーターを追加します。

```cs {.line-numbers highlight=3}
namespace RootNamespace.Applications.WebHost.Features.Users.UseCases
{
    public partial class UserUseCase : UserUseCaseBase
    {
        public UserUseCase(AppDbContext context, IExpandoObjectFactory expandoObjectFactory, IMapper mapper)
            : base(context, expandoObjectFactory, mapper)
        {
        }
    }
}
```

```cs {.line-numbers highlight=4}
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    [AutoGenerateApi(typeof(UserEntity), PrimaryKey = new[] { nameof(UserEntity.Id) })]
    public class UsersController : UsersControllerBase<UserUseCase>
    {
        public UsersController(UserUseCase useCase, IExpandoObjectFactory expandoObjectFactory, IValidatorFactory validatorFactory)
            : base(useCase, expandoObjectFactory, validatorFactory)
        {
        }
    }
}
```

### Validator クラス

Source Generator によって、検証ロジック用のルールセットを表す以下のような `ValidatorRuleSet` クラスが自動生成されます。
`ValidatorRuleSet` クラスは `partial class` として定義されるため、追加でルールセットを定義することができます。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Validators
{
    public static partial class UserValidatorRuleSet
    {
        public const string Add = nameof(Add);
        public const string AddOrUpdate = nameof(AddOrUpdate);
        public const string AddOrUpdateOnBulk = nameof(AddOrUpdateOnBulk);
        public const string DeleteOnBulk = nameof(DeleteOnBulk);
        public const string Update = nameof(Update);
    }
}
```

## 自動生成されたコードのカスタマイズ

### GET API のみを公開する

不要な POST、PATCH、DELETE アクションを `NonActionAttribute` 属性でマークします。
外部キーの定義されていないナビゲーションプロパティを GET API で公開する必要がある場合は、`GET API の実装` の章で説明したように `UseCase` クラスの処理を拡張します。
また、公開するフィールドに承認要件を適用する必要がある場合は `AuthorizationHandler` クラスを実装してください。

### POST、PATCH、DELETE、API を公開する

Source Generator では検証ロジックである `Validator` クラスは自動生成されません。
そのため必要に応じて `AuthorizationHandler` クラス、`Validator` クラスを実装してください。
