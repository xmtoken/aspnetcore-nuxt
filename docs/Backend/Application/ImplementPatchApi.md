# PATCH API の実装

リソースを更新する PATCH API の実装方法を説明します。

## 処理フロー

PATCH API における基本的な処理フローを以下に示します。

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
    FE ->>PL: HTTP PATCH
    PL ->>PL: Model Binding
    PL ->>FV: Validate
    FV-->>PL: Validation Result
alt HasErrors
    PL-->>FE: 400 (Bad Request)
end
    PL ->>CT: Invoke with Model
    CT ->>UC: Invoke with Model
    UC ->>EF: Get Query
    EF ->>DB: Execute Query
    DB-->>EF: Response Entity
    EF-->>UC: Response Entity
alt NotFoundEntity
    UC-->>CT: Response Error
    CT ->>FV: Validate
    FV-->>CT: Validation Result
    CT-->>PL: Validation Result
    PL-->>FE: 400 (Bad Request)
end
    UC ->>EO: Expand Entity
    EO ->>AH: Authorize Field
    AH-->>EO: Authorization Result
    EO ->>EO: Expand Field
    EO-->>UC: Response Expand Entity
    UC ->>AM: Map Entity
    AM-->>UC: Response Entity
    UC ->>EF: Update Query
    EF ->>DB: Execute Query
    DB-->>EF: Response
alt Concurrency
    EF-->>UC: Exception
    UC-->>CT: Response Error
    CT ->>FV: Validate
    FV-->>CT: Validation Result
    CT-->>PL: Validation Result
    PL-->>FE: 400 (Bad Request)
end
    EF-->>UC: Response
    UC-->>CT: Response Entity
    CT-->>PL: Response Entity
    PL-->>FE: 200 (Ok)
```

## PATCH アクション

データの更新処理は基本的に PATCH アクションとして実装します。
アクションのレスポンスはデータの更新に成功した場合は 200 (Ok)、検証エラーの場合は 400 (Bad Request) の２ケースになります。
