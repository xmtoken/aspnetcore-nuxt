# API の実装

API は REST (Representational State Transfer) の原則に倣いリソース単位に機能を構築します。
`RootNamespace.Applications.WebHost.Features` 名前空間にリソース名の複数形で名前区間を区切ります。
例えば、`UserEntity` というリソースの機能を開発する場合 `RootNamespace.Applications.WebHost.Features.Users` が機能の基準となる名前空間となります。

## フォルダー構成

リソース単位に以下のフォルダー構成で機能を実装します。

RootNamespace.Applications.WebHost.Features.{Resources}
├ Authorizations
├ Controllers
├ Models
├ Specifications
├ UseCases
└ Validators

## Authorizations

## Controller

`Controller` は ASP.NET Core MVC のコントローラーのことであり、リクエストを受け付け `UseCase` へ処理を移譲するのが主な責務となります。
`{Resources}Controller` という形でリソース名の複数形と `Controller` というサフィックスを繋げて命名し `Controllers` 名前空間に定義します。
例えば、`UserEntity` のリソースを公開する `Controller` の場合は `UsersController` という命名になります。

１ファイルに多くのコードを実装すると可読性が落ちるため、原則としてコンストラクターや各アクションごとに `partial class` を用いて別々のファイルで定義します。
また、アクションがルーティングパラメーターを受け取る場合は、ルーティングパラメーターと同じフォルダー階層に実装コードのファイルを配置します。ただし、ルーティングパラメーターで作成したフォルダー配下の名前空間は定義しないこととします。
例えば、`UsersController` で複数のリソースを取得する GET アクション、単一のリソースを取得するアクションを実装する場合は以下のようになります。

+ ~/Controllers/UsersController.cs
  `Controller` クラスへの属性の付与やクラスの継承、コンストラクターの実装など基本的な処理は主となる本ファイルに実装します。

  ```cs {.line-numbers}
  namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
  {
      [ApiController]
      [Route("api/[controller]")]
      public partial class UsersController : ControllerBase
      {
          public UsersController()
          {
          }
      }
  }
  ```

+ ~/Controllers/UsersControllerGet.cs
  ファイル名のサフィックスとして Get、Post、Patch、Delete のように実装されているアクションが分かる命名をします。
  特定のステータスを更新するような Patch アクションなどの場合は PatchFavorite などのような命名も検討してください。

  ```cs {.line-numbers}
  namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
  {
      public partial class UsersController
      {
          [HttpGet]
          public UserEntity[] Get()
          {
          }
      }
  }
  ```

+ ~/Controllers/{id}/UsersControllerGet.cs
  ルーティングパラメーターを受け取るため、`{id}` というフォルダー配下にファイルを配置します。
  ただし `id` という名前空間は区切らず上位フォルダーの `UsersController` クラスの `partial class` として扱います。
  `Visual Studio` のコンテキストメニューなどから C# ファイルを新しく追加するとフォルダー階層を基準に名前空間が標準で設定されるため注意してください。

  ```cs {.line-numbers}
  namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
  {
      public partial class UsersController
      {
          [HttpGet]
          [Route("{id:int}")]
          public UserEntity Get([FromRoute] int id)
          {
          }
      }
  }
  ```

### 依存性の注入

基本的にコンストラクターインジェクション以外での他サービスへの依存は禁止とします。
コンストラクターで受け取ったサービスは `private readonly` フィールドに格納し、各アクションで利用します。
例えば、`UserUseCase` をコンストラクターインジェクションで受け取る場合は以下のようになります。

```cs {.line-numbers}
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly UserUseCase UseCase;
        public UsersController(UserUseCase useCase)
        {
            UseCase = useCase;
        }
    }
}
```

基本的に処理はすべて `UseCase` へ移譲するため `Controller` クラス内でデータベースへのアクセスを行わなければならないケースは発生しないはずです。
そのため、`DbContext` サービスを `Controller` へインジェクションすることは禁止します。
また、MVC フレームワークのサービスへ依存していいのはプレゼンテーション層である `Controller` のみとします。
最たる例として `IHttpContextAccessor` サービスがあります。`HttpContext` はマルチスレッドに対応していないため、`UseCase` など他レイヤーで非同期処理を行ったときに予期しないエラーを発生させてしまうことがあります。
そのため、例えば認証されているユーザー名などが `UseCase` などで必要となる場合には値を直接渡してください。

## Model

## Specification

## UseCase

`UseCase` は `Controller` からの要求をもとに `CRUD` 操作などのアプリケーションロジックを実行するのが主な責務となります。
`{Resource}UseCase` という形でリソース名の単数形と `UseCase` というサフィックスを繋げて命名し `UseCases` 名前空間に定義します。
例えば、`UserEntity` のアプリケーションロジックを実装する `UseCase` の場合は `UserUseCase` という命名になります。

１ファイルに多くのコードを実装すると可読性が落ちるため、原則としてコンストラクターや各メソッドごとに `partial class` を用いて別々のファイルで定義します。
例えば、`UserUseCase` で各 `CRUD` 操作を実装する場合は以下のようになります。
※基本的な `CRUD` 操作はすべてベースクラスで実装されているため `CRUD` 操作の例は省略します。

+ ~/UseCases/UserUseCase.cs
  `UseCase` クラスへの属性の付与やクラスの継承、コンストラクターの実装など基本的な処理は主となる本ファイルに実装します。
  基本的な `CRUD` 操作は共通処理として実装されているため `RootNamespace.Applications.WebHost.Core.UseCases` 名前空間で定義されているベースクラスの `UseCaseBase<T>` クラスを継承します。
  `UseCaseBase<T>` クラスのジェネリックパラメーターにはリソースの型を指定します。
  この例では `UserEntity` のリソースに対する `UseCase` の実装なため `UseCaseBase<UserEntity>` となります。
  また、`UseCase` はサービスとして主に `Controller` にインジェクションされるため、依存性の解決を行うために、`IDependencyInjectionService` インターフェイスを実装します。

  ```cs {.line-numbers}
  namespace RootNamespace.Applications.WebHost.Features.Users.UseCases
  {
      public partial class UserUseCase : UseCaseBase<UserEntity>, IDependencyInjectionService
      {
          public UserUseCase(AppDbContext context, IExpandoObjectFactory expandoObjectFactory, IMapper mapper)
              : base(context, expandoObjectFactory, mapper)
          {
          }
      }
  }
  ```

### 依存性の注入

基本は `Controller` の章で述べた内容と同じです。
> 基本的にコンストラクターインジェクション以外での他サービスへの依存は禁止とします。
> コンストラクターで受け取ったサービスは `private readonly` フィールドに格納し、各アクションで利用をします。

### ベースクラスの拡張

`UseCase` のベースクラスである `UseCaseBase<T>` では基本的な `CRUD` 操作が実装されています。
ユースケースによっては機能を拡張する必要がありますが、拡張方法に関しては各 API アクションの実装方法の章で説明します。

## Validator

`Validator` は `Controller` へ届いた要求を検証するのが主な責務となります。
`{Resource}Validator` という形でリソース名の単数形と `Validator` というサフィックスを繋げて命名し `Validators` 名前空間に定義します。
例えば、`UserEntity` の検証ロジックを実装する `Validator` の場合は `UserValidator` という命名になります。
また、一括更新などのようにコレクションの要求に対する検証ロジックを実装する `Validator` の場合は `CollectionValidator` というサフィックスを繋げて命名し `UserCollectionValidator` という命名になります。
検証ロジックの実装には [FluentValidation](https://fluentvalidation.net/) ライブラリを用いるため、`FluentValidation` の詳細な仕様は公式のドキュメントを参照してください。

### 基本的な実装

`Validator` クラスは `FluentValidation` 名前空間の `AbstractValidator<T>` クラスを継承して実装します。
また、`FluentValidation` の [RuleSet](https://docs.fluentvalidation.net/en/latest/rulesets.html) により各 `CRUD` 処理の検証ロジックの共通化を図ります。

例えば、以下のような `UserEntity` クラスに対する検証ロジックの実装を考えてみます。
`Id` プロパティは主キーであり登録時に自動採番されます。
また、`Name` プロパティはログインユーザー名であり一意制約が掛かっていて一度登録すると更新できないとします。
その他の項目は登録時、更新時にも編集が可能な以下のエンティティについての検証ロジックを考えてみましょう。

```cs {.line-numbers}
namespace RootNamespace.Domains.Entities
{
    public class UserEntity
    {
        public int Id { get; set; } /* primary key */
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
```

まずは `RuleSet` の定義を行います。
`{Resource}ValidatorRuleSet` という形でリソース名の単数形と `ValidatorRuleSet` というサフィックスを繋げて命名し `Validators` 名前空間に定義します。
例えば、`UserEntity` の `RuleSet` の場合は `UserValidatorRuleSet` という命名になります。
`RuleSet` クラスにケースごとのルールセット名を定義します。
基本的な `CRUD` 操作は以下のラインナップで網羅できるため以下以外のルールセット名が必要となるケースは稀です。

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

定義した `RuleSet` ごとに検証ロジックを登録します。
以下のように `RuleSet` ごとに実行する検証ロジックを制御することができます。

```cs {.line-numbers}
using RuleSetName = RootNamespace.Applications.WebHost.Features.Users.Validators.UserValidatorRuleSet;
namespace RootNamespace.Applications.WebHost.Features.Users.Validators
{
    public class UserValidator : AbstractValidator<UserEntity>
    {
        RuleSet(ruleSetName: $"{RuleSetName.Update}", () =>
        {
            RuleFor(x => x.Id)
                .NotNull()
                .Custom(/* 存在チェック */);
        });

        RuleSet(ruleSetName: $"{RuleSetName.Add}", () =>
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MaximumLength(256)
                .Custom(/* 一意制約違反チェック */);
        });

        // 複数のルールセットを指定する場合はカンマで区切ります。
        // この場合は Add もしくは Update 何れかのルールセットが指定されている場合に実行されます。
        RuleSet(ruleSetName: $"{RuleSetName.Add},{RuleSetName.Update}", () =>
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .MaximumLength(256);

            RuleFor(x => x.LastName)
                .NotNull()
                .MaximumLength(256);
        });
    }
}
```

実行されるべき `RuleSet` はコントローラーアクションのパラメーター属性として指定します。
※説明のためにアクションを１ファイルにまとめた記載としていますが、実際はコントローラーアクションはファイルを分けて実装します。

```cs {.line-numbers}
using RuleSetName = RootNamespace.Applications.WebHost.Features.Users.Validators.UserValidatorRuleSet;
namespace RootNamespace.Applications.WebHost.Features.Users.Controllers
{
    public partial class UsersController
    {
        [HttpPost]
        public UserEntity Post([CustomizeValidator(RuleSet = RuleSetName.Add)][FromBody] UserEntity model)
        {
        }
        [HttpPatch]
        [Route("{id:int}")]
        public UserEntity Patch([FromRoute] int id, [CustomizeValidator(RuleSet = RuleSetName.Update)][FromBody] UserEntity model)
        {
        }
    }
}
```
