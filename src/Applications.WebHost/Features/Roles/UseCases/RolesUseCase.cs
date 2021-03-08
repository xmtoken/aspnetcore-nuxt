using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Extensions.DependencyInjection;
using AutoMapper;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Roles.UseCases
{
    /// <summary>
    /// ロールに関するユースケースを表します。
    /// </summary>
    public partial class RolesUseCase : IDependencyInjectionService
    {
        /// <summary>
        /// <see cref="AppDbContext"/> オブジェクトを表します。
        /// </summary>
        private readonly AppDbContext Context;

        /// <summary>
        /// <see cref="IMapper"/> オブジェクトを表します。
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// <see cref="RolesUseCase"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        /// <param name="mapper"><see cref="IMapper"/> オブジェクト。</param>
        public RolesUseCase(AppDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
    }
}
