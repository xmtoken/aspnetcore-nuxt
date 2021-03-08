# DELETE API の実装

リソースを削除する DELETE API の実装方法を説明します。

## 処理フロー

DELETE API における基本的な処理フローを以下に示します。

```mermaid
sequenceDiagram
    participant FE as Frontend
    participant PL as ASP.NET Core Pipeline
    participant FV as FluentValidation
    participant CT as Controller
    participant UC as UseCase
    participant EF as EntityFrameworkCore
    participant DB as Database
    FE ->>PL: HTTP DELETE
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
    UC-->>CT: Response Ok
    CT-->>PL: Response
    PL-->>FE: 200 (Ok)
end
    UC ->>EF: Delete Query
    EF ->>DB: Execute Query
    DB-->>EF: Response
alt Concurrency
    EF-->>UC: Exception
    UC-->>CT: Response Ok
    CT-->>PL: Response
    PL-->>FE: 200 (Ok)
end
    EF-->>UC: Response
    UC-->>CT: Response
    CT-->>PL: Response
    PL-->>FE: 200 (Ok)
```

## DELETE アクション

データの削除処理は基本的に DELETE アクションとして実装します。
アクションのレスポンスはデータの削除に成功した場合は 200 (Ok) 、検証エラーの場合は 400 (Bad Request) の２ケースになります。
