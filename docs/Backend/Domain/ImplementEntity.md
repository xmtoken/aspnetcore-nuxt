# エンティティの実装

基本的にデータベースのテーブルに１対１で対応するエンティティクラスを定義します。
`{TableName}Entity` という形でテーブル名と `Entity` というサフィックスを繋げて命名し `RootNamespace.Domains.Entities` 名前空間に定義します。
例えば、`User` テーブルのエンティティの場合は `UserEntity` という命名になります。
ただし、`EntityFrameworkCore` のテーブル分割機能を使ってエンティティクラスの定義を分ける場合には、上記ルールを遵守しつつそれぞれ適切な命名をしてください。

## エンティティのメンバーの型

エンティティクラスはアプリケーション層の API のモデルとして公開するため、メンバーの値型はすべて `Nullable<T>` 型として定義してください。
値型として定義すると、ASP.NET Core MVC フレームワークによってモデルバインディングされたモデルを検証するときに、値型が既定値の場合にリクエストが既定値だったのか、モデルバインディングが行われず既定値が使われているのか判別がつかないためです。

```cs {.line-numbers}
namespace RootNamespace.Domains.Entities
{
    public class UserEntity
    {
        // データベースで NotNull 制約が付いていても値型はすべて Nullable<T> 型とする。
        public int? Id { get; set; }
    }
}
```

## エンティティの構成

エンティティクラスごとにエンティティの構成を `fluent API` を使用して定義します。
属性での構成は原則は禁止とします。
`{EntityName}TypeConfiguration` という形でテーブル名と `TypeConfiguration` というサフィックスを繋げて命名し `RootNamespace.Domains.Data.EntityTypeConfigurations` 名前空間に定義します。
例えば、`User` テーブルの `UserEntity` クラスの構成の場合は `UserEntityTypeConfiguration` という命名になります。
定義方法は `EntityFrameworkCore` の [ドキュメント](https://docs.microsoft.com/ja-jp/ef/core/modeling/) を参照してください。

## エンティティの構成の記述順序

エンティティの構成の記述は以下の順序で記述することします。

+ Tables
+ Indexes
+ Properties
+ Filters
+ Relationships

以下のエンティティを例に示します。

```cs {.line-numbers}
namespace RootNamespace.Domains.Entities
{
    public class SampleEntity
    {
        public int? Id { get; set; }

        public int? IndexValue { get; set; }

        public int? CompositeIndexValue1 { get; set; }

        public int? CompositeIndexValue2 { get; set; }

        public int? UniqueIndexValue { get; set; }

        public string NullableValue { get; set; }

        public string MaxLengthValue { get; set; }

        public string FullLengthValue { get; set; }

        public string FixedLengthValue { get; set; }

        public string NotUnicodeValue { get; set; }

        public decimal? Number { get; set; }

        public string ConcurrencyValue { get; set; }

        public SampleOneRelationEntity SampleOne { get; set; }

        public ICollection<SampleManyRelationEntity> SampleMany { get; set; }

        public ICollection<SampleCascadeRelationEntity> SampleCascade { get; set; }
    }

    public class SampleOneRelationEntity
    {
        public int? Id { get; set; }

        public int? SampleId { get; set; }
    }

    public class SampleManyRelationEntity
    {
        public int? Id { get; set; }

        public int? SampleId { get; set; }
    }

    public class SampleCascadeRelationEntity
    {
        public int? Id { get; set; }

        public int? SampleId { get; set; }
    }
}
```

```cs {.line-numbers}
namespace RootNamespace.Domains.Data.EntityTypeConfigurations
{
    public class SampleEntityTypeConfiguration : IEntityTypeConfiguration<SampleEntity>
    {
        public void Configure(EntityTypeBuilder<SampleEntity> builder)
        {
            // テーブルと主キーを構成します。
            builder.ToTable("Samples")
                   .HasKey(x => x.Id);

            // インデックスを構成します。
            builder.HasIndex(x => x.IndexValue);

            // 複合インデックスを構成します。
            builder.HasIndex(x => new { x.CompositeIndexValue1, x.CompositeIndexValue2 });

            // 一意インデックスを構成します。
            builder.HasIndex(x => x.UniqueIndexValue)
                   .IsUnique();

            // Identity を構成します。
            builder.Property(x => x.Id)
                   .IsRequired()
                   .UseIdentityColumn();

            // NotNull を構成します。
            builder.Property(x => x.IndexValue)
                   .IsRequired();

            builder.Property(x => x.CompositeIndexValue1)
                   .IsRequired();

            builder.Property(x => x.CompositeIndexValue2)
                   .IsRequired();

            builder.Property(x => x.UniqueIndexValue)
                   .IsRequired();

            // Null 許容の場合は IsRequired() を呼び出しません。
            builder.Property(x => x.NullableValue);

            // 文字の桁数を構成します。
            builder.Property(x => x.MaxLengthValue)
                   .HasMaxLength(100);

            // 文字の桁数が Max の場合は HasMaxLength() を呼び出しません。
            builder.Property(x => x.FullLengthValue)
                   .IsRequired();

            // 文字を固定長として構成します。
            builder.Property(x => x.FixedLengthValue)
                   .HasMaxLength(100)
                   .IsFixedLength();

            // 文字を非 UNICODE として構成します。
            builder.Property(x => x.FixedLengthValue)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // 数値の有効桁数と小数部桁数を構成します。
            builder.Property(x => x.Number)
                   .HasPrecision(10, 3);

            // コンカレンシートークンとして構成します。
            builder.Property(x => x.ConcurrencyValue)
                   .IsConcurrencyToken();

            // １対１のリレーションを構成します。
            builder.HasOne(x => x.SampleOne)
                   .WithOne()
                   .HasForeignKey<SampleOneRelationEntity>(x => x.SampleId);

            // １対Ｎのリレーションを構成します。
            builder.HasMany(x => x.SampleMany)
                   .WithOne()
                   .HasForeignKey(x => x.SampleId);

            // リレーションのカスケード動作を構成します。
            builder.HasMany(x => x.SampleCascade)
                   .WithOne()
                   .HasForeignKey(x => x.SampleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
```
