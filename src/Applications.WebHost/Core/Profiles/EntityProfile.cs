using AspNetCoreNuxt.Applications.WebHost.Core.Entities;
using AutoMapper;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Profiles
{
    /// <summary>
    /// <see cref="IEntity{T}"/> インタフェースを実装したクラスのマッピングを提供します。
    /// </summary>
    public class EntityProfile : Profile
    {
        /// <summary>
        /// <see cref="EntityProfile"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public EntityProfile()
        {
            foreach (var entityType in typeof(Startup).Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract))
            {
                var entityInterfaces = entityType.GetInterfaces();
                var entityInterface = entityInterfaces.SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntity<>));
                if (entityInterface == null)
                {
                    continue;
                }

                // データモデルのエンティティから API で公開するモデルへのマッピングを構成します。
                CreateMap(entityInterface.GenericTypeArguments[0], entityType)
                    .ForAllMembers(x =>
                    {
                        // ネストされたオブジェクトのプロパティをキーとしてクエリのソートを行う場合に、
                        // 生成される式ツリーへ null チェック処理が挟まることによるクエリ変換の失敗を防ぎます。
                        // https://stackoverflow.com/questions/39950128/automapper-projection-with-linq-orderby-child-property-error/39965284
                        x.AllowNull();

                        // API の要求元から取得項目を指定できるように項目の展開を明示的にします。
                        x.ExplicitExpansion();
                    });

                // API で公開するモデルから同一のモデルへのマッピングを構成します。
                // 展開しない項目をキーとしてクエリのソートを行う場合に、一度ソートキーを含めて API で公開するモデルへマッピングし、
                // ソート後にキーを省いて同一モデル間のマッピングを行うために構成します。
                CreateMap(entityType, entityType)
                    .ForAllMembers(x => x.ExplicitExpansion());
            }
        }
    }
}
