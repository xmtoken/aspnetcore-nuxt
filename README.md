# README

1. アーキテクチャ概要
   1. [技術スタック](docs/Architecture/TechnologyStack.md)
2. バックエンドの実装
   1. [プロジェクトの構成](docs/Backend/ProjectStructure.md)
   2. [コーディング規約](docs/Backend/CodingRule.md)
   3. ドメイン層の実装
      1. [エンティティの実装](docs/Backend/Domain/ImplementEntity.md)
      2. [データモデルのマイグレーション](docs/Backend/Domain/Migration.md)
   4. インフラストラクチャ層の実装
   5. アプリケーション層の実装
      1. [API の実装](docs/Backend/Application/ImplementApi.md)
         1. [GET API の実装](docs/Backend/Application/ImplementGetApi.md)
         2. [POST API の実装](docs/Backend/Application/ImplementPostApi.md)
         3. [PATCH API の実装](docs/Backend/Application/ImplementPatchApi.md)
         4. [DELETE API の実装](docs/Backend/Application/ImplementDeleteApi.md)
      2. [Source Generator による API の自動実装](docs/Backend/Application/ImplementSourceGenerator.md)
      3. [排他制御](docs/Backend/Application/ConcurrencyToken.md)
      4. [検証ロジック](docs/Backend/Application/Validation.md)
      5. [メッセージ管理]
   6. オーケストレーション層の実装
   7. ロギング
   8. Swagger UI
   9. NSwag.MSBuild による API I/F の自動実装
3. フロントエンドの実装
   1. [プロジェクトの構成](docs/Frontend/ProjectStructure.md)
   2. コーディング規約
   3. レイアウト
      1. [Rendering パフォーマンス](docs/Frontend/Layout/RenderingPerformance.md)
      2. [Form](docs/Frontend/Layout/Form.md)
   4. API 要求
      1. GET API 要求
      2. POST API 要求
      3. PATCH API 要求
      4. DELETE API 要求
   5. 検証ロジック
