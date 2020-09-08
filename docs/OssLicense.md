# OSS License

本システムで扱う OSS のパッケージとそのライセンス体系を以下に示す。

## NuGet

NuGet パッケージのライセンス体系を以下に示す。
各パッケージのバージョンは `~/src/Directory.Build.targets` を参照すること。

| Package                                               | License                           |
| ----------------------------------------------------- | --------------------------------- |
| AutoMapper                                            | MIT License                       |
| AutoMapper.Extensions.Microsoft.DependencyInjection   | MIT License                       |
| CsvHelper                                             | Apache License 2.0 & MS-PL        |
| FluentValidation                                      | Apache License 2.0                |
| FluentValidation.AspNetCore                           | Apache License 2.0                |
| Microsoft.AspNetCore.Mvc.NewtonsoftJson               | Apache License 2.0                |
| Microsoft.AspNetCore.Mvc.Versioning                   | MIT License                       |
| Microsoft.AspNetCore.SpaServices.Extensions           | Apache License 2.0                |
| Microsoft.CodeAnalysis.FxCopAnalyzers                 | Apache License 2.0                |
| Microsoft.EntityFrameworkCore.Design                  | Apache License 2.0                |
| Microsoft.EntityFrameworkCore.Relational              | Apache License 2.0                |
| Microsoft.EntityFrameworkCore.SqlServer               | Apache License 2.0                |
| Microsoft.Extensions.DependencyInjection.Abstractions | Apache License 2.0                |
| Microsoft.Extensions.Hosting                          | Apache License 2.0                |
| Microsoft.Extensions.Identity.Core                    | Apache License 2.0                |
| Newtonsoft.Json                                       | MIT License                       |
| NSwag.AspNetCore                                      | MIT License                       |
| NSwag.CodeGeneration.TypeScript                       | MIT License                       |
| NSwag.Generation                                      | MIT License                       |
| NSwag.MSBuild                                         | MIT License                       |
| Oracle.EntityFrameworkCore                            | Oracle Technology Network License |
| Polly                                                 | BSD 3-Clause License              |
| Serilog.AspNetCore                                    | Apache License 2.0                |
| Serilog.Sinks.Async                                   | Apache License 2.0                |
| StyleCop.Analyzers                                    | MIT License                       |

### 推移的依存パッケージのライセンス統計情報

推移的に依存するすべての NuGet パッケージに対するライセンスの統計情報を以下に示す。
ライセンスチェッカーの仕様に関しては [GitHub](https://github.com/aaronpowell/dotnet-delice) を参照すること。

`dotnet delice | Where-Object {$_ -like "*License Expression*"} | Sort-Object | Get-Unique`

```
License Expression: Apache-2.0
License Expression: BSD-3-Clause
License Expression: Microsoft Software License
License Expression: MIT
License Expression: MS-PL OR Apache-2.0
License Expression: Project References
License Expression: Unable to determine
```

## npm

npm パッケージのライセンス体系を以下に示す。
各パッケージのバージョンは `~/src/Applications.WebHost/ClientApp/package.json` を参照すること。

| Package                           | License              |
| --------------------------------- | -------------------- |
| @mdi/js                           | MIT License          |
| @nuxt/types                       | MIT License          |
| @nuxt/typescript-build            | MIT License          |
| @nuxt/typescript-runtime          | MIT License          |
| @nuxtjs/auth                      | MIT License          |
| @nuxtjs/axios                     | MIT License          |
| @nuxtjs/eslint-config-typescript  | MIT License          |
| @nuxtjs/vuetify                   | MIT License          |
| @types/deep-equal                 | MIT License          |
| @types/file-saver                 | MIT License          |
| @types/fontfaceobserver           | MIT License          |
| @types/lodash                     | MIT License          |
| @types/nuxtjs__auth               | MIT License          |
| @types/ts-nameof                  | MIT License          |
| content-disposition               | MIT License          |
| dayjs                             | MIT License          |
| deep-equal                        | MIT License          |
| eslint                            | MIT License          |
| eslint-config-prettier            | MIT License          |
| eslint-loader                     | MIT License          |
| eslint-plugin-import              | MIT License          |
| eslint-plugin-prettier            | MIT License          |
| eslint-plugin-vue                 | MIT License          |
| file-saver                        | MIT License          |
| fontfaceobserver                  | BSD 2-Clause License |
| http-status-codes                 | MIT License          |
| license-checker                   | BSD 3-Clause License |
| npm-run-all                       | MIT License          |
| numbro                            | MIT License          |
| nuxt                              | MIT License          |
| prettier                          | MIT License          |
| qs                                | BSD 3-Clause License |
| stylelint                         | MIT License          |
| stylelint-config-prettier         | MIT License          |
| stylelint-config-recommended-scss | MIT License          |
| stylelint-config-standard         | MIT License          |
| stylelint-order                   | MIT License          |
| stylelint-prettier                | MIT License          |
| stylelint-scss                    | MIT License          |
| ts-nameof                         | MIT License          |
| ts-nameof-loader                  | MIT License          |
| v-mask                            | MIT License          |
| vee-validate                      | MIT License          |

### 推移的依存パッケージのライセンス統計情報

推移的に依存するすべての npm パッケージに対するライセンスの統計情報を以下に示す。
ライセンスチェッカーの仕様に関しては [GitHub](https://github.com/davglass/license-checker) を参照すること。

`npm run license:summary --prefix src/Applications.WebHost/ClientApp`

```
├─ MIT: 1181
├─ ISC: 93
├─ BSD-2-Clause: 28
├─ CC0-1.0: 24
├─ BSD-3-Clause: 18
├─ Apache-2.0: 13
├─ (MIT OR CC0-1.0): 4
├─ BSD*: 2
├─ Public Domain: 2
├─ (MIT OR Apache-2.0): 1
├─ CC-BY-4.0: 1
├─ BSD: 1
├─ GPL-3.0-or-later OR MIT: 1
├─ (WTFPL OR MIT): 1
├─ (MIT AND Zlib): 1
├─ (MIT AND BSD-3-Clause): 1
├─ CC-BY-3.0: 1
├─ (MIT AND CC-BY-3.0): 1
├─ MIT*: 1
└─ 0BSD: 1
```

### ライセンス補足情報

推移的に依存するすべての npm パッケージのうち、自動的にライセンスを識別できないパッケージに関する情報を以下に示す。

`npm run license:unknown --prefix src/Applications.WebHost/ClientApp`

```
├─ css-select@1.2.0
│  ├─ licenses: BSD*
│  ├─ repository: https://github.com/fb55/css-select
│  ├─ publisher: Felix Boehm
│  ├─ email: me@feedic.com
│  ├─ path: C:\Users\xm\LocalRepository\aspnetcore-nuxt\src\Applications.WebHost\ClientApp\node_modules\renderkid\node_modules\css-select
│  └─ licenseFile: C:\Users\xm\LocalRepository\aspnetcore-nuxt\src\Applications.WebHost\ClientApp\node_modules\renderkid\node_modules\css-select\LICENSE
├─ domutils@1.5.1
│  ├─ licenses: BSD*
│  ├─ repository: https://github.com/FB55/domutils
│  ├─ publisher: Felix Boehm
│  ├─ email: me@feedic.com
│  ├─ path: C:\Users\xm\LocalRepository\aspnetcore-nuxt\src\Applications.WebHost\ClientApp\node_modules\renderkid\node_modules\domutils
│  └─ licenseFile: C:\Users\xm\LocalRepository\aspnetcore-nuxt\src\Applications.WebHost\ClientApp\node_modules\renderkid\node_modules\domutils\LICENSE
└─ trim@0.0.1
   ├─ licenses: MIT*
   ├─ publisher: TJ Holowaychuk
   ├─ email: tj@vision-media.ca
   ├─ path: C:\Users\xm\LocalRepository\aspnetcore-nuxt\src\Applications.WebHost\ClientApp\node_modules\trim
   └─ licenseFile: C:\Users\xm\LocalRepository\aspnetcore-nuxt\src\Applications.WebHost\ClientApp\node_modules\trim\Readme.md
```

| Package          | License              | Document                                                 |
| ---------------- | -------------------- | -------------------------------------------------------- |
| css-select@1.2.0 | BSD 2-Clause License | <https://github.com/fb55/css-select/blob/master/LICENSE> |
| domutils@1.5.1   | BSD 2-Clause License | <https://github.com/fb55/domutils/blob/master/LICENSE>   |
| trim@0.0.1       | MIT License          | <https://www.npmjs.com/package/trim#license>             |
