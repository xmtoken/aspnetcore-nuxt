# プロジェクトの構成

プロジェクトは大きく分けて以下の４つに分類されます。

## Applications プロジェクト

アプリケーションロジックを実装するシステムのメインエントリになります。主に API サービスやバッチプログラムなどが `Applications` プロジェクトに該当します。`Applications` プロジェクトは `Applications` プロジェクト以外のすべてのプロジェクトに対して依存することができます、ただし、`Infrastructures` プロジェクトで実装されているインフラストラクチャ層のサービスはインターフェイス越しに操作を行います。

## Domains プロジェクト

ドメインロジックを実装します。`Domains` プロジェクトは `Extensions` プロジェクト以外への依存は許されません。ドメインロジックは基本的に `POCO (Plain Old CLR Object)` で実装される必要があります。

## Extensions プロジェクト

ドメインロジック、アプリケーションロジックを一切含まない、汎用的なクラスやヘルパーメソッドが含まれます。`Extensions` プロジェクトは `Extensions` プロジェクト以外へ依存することは許されません。`Extensions` プロジェクト以外へ依存する場合、それは `Extensions` プロジェクト以外へ実装すべき内容になります。`Extensions` プロジェクトに実装する内容かの１つの判断は実装コードを変更せずに他プロジェクトへ転用できるかどうかです。

## Infrastructures プロジェクト

`Domains` プロジェクトや `Extensions` プロジェクトで抽象化された外部サービスに対する実装コードが含まれます。外部サービスとはアプリケーションの外界を表し、ディスクＩ／Ｏ、メールサービス、外部 API サービスなどを指します。すべての外部サービスを抽象化するかどうかはサービスへの依存度や開発工数をもとに検討してください。また、本プロジェクトで実装されたインフラストラクチャ層のサービスは抽象化されたインターフェイス越しに操作されるため、直接操作されることはありません。
