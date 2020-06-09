using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Extensions.DependencyInjection;
using AutoMapper;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
{
    /// <summary>
    /// ユーザーに関するユースケースを表します。
    /// </summary>
    public partial class UserUseCase : IApplicationService
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
        /// <see cref="UserUseCase"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        /// <param name="mapper"><see cref="IMapper"/> オブジェクト。</param>
        public UserUseCase(AppDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
    }
}
