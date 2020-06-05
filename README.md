# aspnetcore-nuxt

## 開発環境

ランタイムとして、`.NET Core 3.1.4+` と `Node.js 12.16.2+` のインストールが必要です。
IDE は `Visual Studio Code 1.45.1+` を利用します。
高度な IDE サポートを望む場合は、C# の開発に `Visual Studio 16.5.5+` を利用できます。

## アプリケーションの実行

1. データベースへの接続情報を以下のファイルへ設定します。
   ```
   ~/src/Applications.WebHost/appsettings.Development.json
   ~/src/Domains.Data.Design/appsettings.json
   ```
2. dotnet tool をインストールします。
   ```
   dotnet tool restore
   ```
3. ASP.NET Core HTTPS 開発証明書をインストールします。
   ```
   dotnet dev-certs https --trust
   ```
4. npm module をインストールします。
   ```
   npm ci --prefix src/Applications.WebHost/ClientApp
   ```
5. データベースを構成していない、もしくは構成が古い場合はマイグレーションします。
   ```
   dotnet ef database update -p src/Domains.Data.Design
   ```
6. Nuxt.js を <http://localhost:3000> でホスティングします。
   ```
   npm run dev --prefix src/Applications.WebHost/ClientApp
   ```
7. ASP.NET Core を <https://localhost:5001> でホスティングします。
   ```
   dotnet run -p src/Applications.WebHost
   ```

## アプリケーションの修正

### VSCode

VSCode でフォルダーを開くと開発に必要な拡張機能が reccomend されます。
インテリセンスや Linter のサポートを受けるために、すべての拡張機能をインストールする必要があります。
reccomend の一覧は `~/.vscode/extensions.json` で確認できます。

### Nuxt.js

Nuxt.js はインテリセンスや Linter のサポートを受けるために VisualStudio ではなく VSCode で開発してください。
また、VSCode で開くフォルダーはプロジェクト直下ではなく、`~/src/Applications.WebHost/ClientApp` フォルダーを開いてください。
違う階層を基準に開いた場合は、vetur によるインテリセンスのサポートを受けれません。

## その他

+ [Architecture](docs/Architecture.md)
+ [Coding Guideline](docs/CodingGuideline.md)
+ [OSS License](docs/OssLicense.md)
+ [Versioning](docs/Versioning.md)
