using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AutoMapper;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    /// <summary>
    /// ユースケースのベースクラスを表します。
    /// </summary>
    /// <typeparam name="T">エンティティの型。</typeparam>
    public abstract partial class UseCaseBase<T>
        where T : class
    {
        /// <summary>
        /// <see cref="AppDbContext"/> オブジェクトを取得します。
        /// </summary>
        protected AppDbContext Context { get; }

        /// <summary>
        /// <see cref="IExpandoObjectFactory"/> オブジェクトを取得します。
        /// </summary>
        protected IExpandoObjectFactory ExpandoObjectFactory { get; }

        /// <summary>
        /// <see cref="IMapper"/> オブジェクトを取得します。
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// <see cref="UseCaseBase{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        /// <param name="expandoObjectFactory"><see cref="IExpandoObjectFactory"/> オブジェクト。</param>
        /// <param name="mapper"><see cref="IMapper"/> オブジェクト。</param>
        protected UseCaseBase(AppDbContext context, IExpandoObjectFactory expandoObjectFactory, IMapper mapper)
        {
            Context = context;
            ExpandoObjectFactory = expandoObjectFactory;
            Mapper = mapper;
        }
    }
}
