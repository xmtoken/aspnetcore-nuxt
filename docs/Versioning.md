# Versioning

本システムにおけるバージョニングの仕様を以下に示す。

## Assembly バージョン

アセンブリのバージョニングは[セマンティックバージョニング](https://semver.org/lang/ja/)の仕様に準拠すること。
また、バージョンは `Directory.Build.props` で管理し、すべてのアセンブリで統一する。

## API バージョン

API はリクエストに API バージョンを要求するように設定し、フロントエンドにおいてバージョンの更新を検知できるようにする。
API で後方互換性のある修正を行う場合は、フロントエンドに影響がないため API バージョンの更新を行わないこと。逆に、後方互換性のない修正を行う場合は、API バージョンを更新しフロントエンドでアプリケーションの更新を検知できるようにする。
API バージョンは `Applications.WebHost.Core.Mvc.AppApiVersion` 列挙体で管理し、既定の API バージョン以外のアクションには、`Applications.WebHost.Core.Mvc.AppApiVersionAttribute` 属性で API バージョンを指定する。

## SPA バージョン

SPA は初回のリクエストでリソースを全て取得する特性により、新しいバージョンをデプロイしても更新が行われない。デプロイされた最新バージョンを反映させるために、ルーティング時にSPA のバージョンを検証し、自動でページの再読み込みを行わせることとする。
SPA バージョンは `Applications.WebHost.Core.Mvc.SpaApiVersion` 列挙体で管理し、フロントエンドは `Applications.WebHost.Features.Versioning.Controllers.Get()` アクションからバージョンの検証を行う。
