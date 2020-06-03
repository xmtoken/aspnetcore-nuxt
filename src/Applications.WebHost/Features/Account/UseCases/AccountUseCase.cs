using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Extensions.DependencyInjection;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.UseCases
{
    /// <summary>
    /// アカウントに関するユースケースを表します。
    /// </summary>
    public partial class AccountUseCase : IApplicationService
    {
        /// <summary>
        /// <see cref="AppDbContext"/> オブジェクトを表します。
        /// </summary>
        private readonly AppDbContext Context;

        /// <summary>
        /// <see cref="AccountUseCase"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        public AccountUseCase(AppDbContext context)
        {
            Context = context;
        }
    }
}
