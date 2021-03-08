# POST API の実装

リソースを登録する POST API の実装方法を説明します。

## 処理フロー

POST API における基本的な処理フローを以下に示します。

```mermaid
sequenceDiagram
    participant FE as Frontend
    participant PL as ASP.NET Core Pipeline
    participant FV as FluentValidation
    participant CT as Controller
    participant UC as UseCase
    participant EO as ExpandObjectFactory
    participant AH as AuthorizationHandler
    participant AM as AutoMapper
    participant EF as EntityFrameworkCore
    participant DB as Database
    FE ->>PL: HTTP POST
    PL ->>PL: Model Binding
    PL ->>FV: Validate
    FV-->>PL: Validation Result
alt HasErrors
    PL-->>FE: 400 (Bad Request)
end
    PL ->>CT: Invoke with Model
    CT ->>UC: Invoke with Model
    UC ->>EO: Expand Entity
    EO ->>AH: Authorize Field
    AH-->>EO: Authorization Result
    EO ->>EO: Expand Field
    EO-->>UC: Response Expand Entity
    UC ->>AM: Map Entity
    AM-->>UC: Response Entity
    UC ->>EF: Create Query
    EF ->>DB: Execute Query
alt UniqueConstraintViolation
    DB-->>EF: Exception
    EF-->>UC: Exception
    UC-->>CT: Response Error
    CT ->>FV: Validate
    FV-->>CT: Validation Result
    CT-->>PL: Validation Result
    PL-->>FE: 400 (Bad Request)
end
    DB-->>EF: Response
    EF-->>UC: Response
    UC-->>CT: Response Entity
    CT-->>PL: Response Entity
    PL-->>FE: 201 (Created)
```

## POST アクション

データの登録処理は基本的に POST アクションとして実装します。
アクションのレスポンスはデータの登録に成功した場合は 201 (Created)、検証エラーの場合は 400 (Bad Request) の２ケースになります。
