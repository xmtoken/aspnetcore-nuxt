# Coding Guideline

本システムにおけるコーディングガイドラインを以下に示す。

## C\#

### コーディング規則

以下のガイドラインを一読し、C# における一般的な記述に倣い、可読性を損なわないように気を付けること。

+ [クラス ライブラリ開発のデザイン ガイドライン](https://docs.microsoft.com/ja-jp/previous-versions/dotnet/netframework-3.5/ms229042(v=vs.90))
+ [C# のコーディング規則 (C# プログラミング ガイド)](https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)

また、上記ガイドラインに記載のない `private field` の命名についてはパスカルケースで記述する。
上記以外に明確なコーディング規則は設けないが、コード分析による警告は原則として抑止をせずに、すべて適切に処置を行うこと。

### コメント

すべてのクラス、インターフェイス、そのメンバーなどについて、アクセス修飾子に関わらず、XML コメントを記述すること。
XML コメントを含むすべてのコメントにおいて、英数字記号は半角文字を用い、半角文字と全角文字の間には半角空白を含めること。
また、コメントにはコードを日本語に訳しただけのものではなく、ロジックの意味や意図を伝える内容を記述すること。

## JavaScript/TypeScript

### コーディング規則

以下のガイドラインを一読し、JavaScript/TypeScript における一般的な記述に倣い、可読性を損なわないように気を付けること。

+ [Google JavaScript Style Guide](https://google.github.io/styleguide/javascriptguide.xml)
+ [Google JavaScript Style Guide (日本語訳)](https://w.atwiki.jp/aias-jsstyleguide2/pages/1.html)
+ [TypeScript Deep Dive](https://typescript-jp.gitbook.io/deep-dive/)

上記以外に明確なコーディング規則は設けないが、eslint、stylelint による警告は原則として抑止をせずに、すべて適切に処置を行うこと。
