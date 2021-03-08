# README

## 開発環境

開発を行うには [.NET Core SDK 3.1.403+](https://dotnet.microsoft.com/) と [Node.js 12.19.0+](https://nodejs.org/ja/) のインストールが必要です。
IDE は [Visual Studio Code 1.51.1+](https://azure.microsoft.com/ja-jp/products/visual-studio-code/) を利用します。
C# の開発には [Visual Studio 16.8.1+](https://visualstudio.microsoft.com/ja/downloads/) も利用できます。

## アプリケーションの実行

1. データベースへの接続情報を以下のファイルへ設定します。

    ```
    ~/src/Applications.WebHost/appsettings.Development.json
    ~/src/Domains.Data.Design/appsettings.json
    ```

2. dotnet tool をインストールします。

   ```
   CLI > dotnet tool restore
   ```

3. ASP.NET Core HTTPS 開発証明書 をインストールします。

   ```
   CLI > dotnet dev-certs https --trust
   ```

4. npm package をインストールします。

   ```
   CLI > npm ci --prefix src/Applications.WebHost/ClientApp
   ```

5. データベースをマイグレーションします。

   ```
   CLI > dotnet ef database update -p src/Domains.Data.Design
   ```

6. Nuxt.js を <http://localhost:3000> でホスティングします。

   ```
   CLI > npm run dev --prefix src/Applications.WebHost/ClientApp
   ```

7. ASP.NET Core を <https://localhost:5001> でホスティングします。

   ```
   CLI > dotnet run -p src/Applications.WebHost
   ```

## アプリケーションの修正

### VSCode

VSCode でフォルダーを開くと開発に必要な拡張機能が reccomend されます。
インテリセンスや Linter のサポートを受けるために、すべての拡張機能をインストールする必要があります。
reccomend の一覧は `~/.vscode/extensions.json` で確認できます。

### Nuxt.js

Nuxt.js はインテリセンスや Linter のサポートを受けるために VisualStudio ではなく VSCode で開発してください。
また、VSCode で開くフォルダーはプロジェクト直下ではなく、`~/src/Applications.WebHost/ClientApp` フォルダーを開いてください。
違う階層を基準に開いた場合は、[vetur](https://github.com/vuejs/vetur) によるインテリセンスのサポートを受けれません。

## その他

+ [Architecture](docs/Architecture.md)
+ [Coding Rules](docs/CodingRules.md)
+ [OSS Licenses](docs/OssLicenses.md)
+ [Problems](docs/Problems.md)
+ [Versioning](docs/Versioning.md)
