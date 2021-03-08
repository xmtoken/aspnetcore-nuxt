# コーディング規約

## Roslyn Analyzer

Roslyn Analyzer を導入しているため、出力された警告は抑止をせずにすべて適切に処置をしてください。

## 命名規則

ハンガリアン記法、スネークケースは禁止とします。
メソッドのパラメーター名、変数名はロワーキャメルケース、それ以外についてはすべてパスカルケースを使用してください。
`private` なフィールド名についても、アンダースコアーで始めるロワーキャメルケースではなく、パスカルケースを使用してください。
`Tuple` の要素名についても、ロワーキャメルケースではなく、パスカルケースを使用してください。

### ラムダ式のパラメーター名

ラムダ式のパラメーターが１つの場合で式が複雑でなく、特に命名の必要性がない場合には `x` をパラメーター名としてください。
ただし、ラムダ式がネストした場合に `x`、`y` と命名するのは禁止とします。
その場合には適切な命名をしてください。

## コメント

### XML コメント

アクセシビリティに関係なくすべてのメンバーに関して XML コメントを記載してください。
XML コメントにおいて型やパラメーターなどを参照する場合は、`<paramref>`、`<see>`、`<typeparamref>` などの [ドキュメントタグ](https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/xmldoc/) を活用してください。

#### プロパティの XML コメント

プロパティの XML コメントの定型文は `{PropertyName}を取得または設定します。`、`{PropertyName}を取得します。` です。
取得、設定の表現はプロパティとアクセシビリティが同じアクセサーのみ表現します。

```cs {.line-numbers}
/// <summary>
/// ユーザー名を取得または設定します。
/// </summary>
public string UserName { get; set; }
/// <summary>
/// ユーザー名を取得します。
/// </summary>
public string UserName { get; protected set; }
/// <summary>
/// ユーザー名を取得します。
/// </summary>
public string UserName { get; private set; }
/// <summary>
/// ユーザー名を取得または設定します。
/// </summary>
protected string UserName { get; set; }
/// <summary>
/// ユーザー名を取得します。
/// </summary>
protected string UserName { get; private set; }
```

#### フィールドの XML コメント

フィールドの XML コメントの定型文は `{PropertyName}を表します。` です。

```cs {.line-numbers}
/// <summary>
/// ユーザー名表します。
/// </summary>
private string UserName;
```

## アクセシビリティ

### フィールドのアクセシビリティ

フィールドのアクセシビリティは原則として `private` です。
`private` 以外のアクセシビリティを適用する場合はプロパティで定義することを検討してください。
ただし、構造体をプロパティの `get` アクセサー経由で取得することで、値のコピーが発生することが望ましくないケースでは、慎重に検討した上でフィールドのアクセシビリティを上げてください。

### プロパティのアクセシビリティ

プロパティのアクセシビリティは原則として `private` 以外です。
`private` アクセシビリティを適用する場合はフィールドで定義することを検討してください。
