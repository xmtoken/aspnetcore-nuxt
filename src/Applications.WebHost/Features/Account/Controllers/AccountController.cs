using AspNetCoreNuxt.Applications.WebHost.Features.Account.UseCases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Controllers
{
    /// <summary>
    /// アカウントに関する API を提供します。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class AccountController : ControllerBase
    {
        /// <summary>
        /// <see cref="AccountUseCase"/> オブジェクトを表します。
        /// </summary>
        private readonly AccountUseCase UseCase;

        /// <summary>
        /// <see cref="IAuthenticationSchemeProvider"/> オブジェクトを表します。
        /// </summary>
        private readonly IAuthenticationSchemeProvider AuthenticationSchemeProvider;

        /// <summary>
        /// <see cref="AccountController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="useCase"><see cref="AccountUseCase"/> オブジェクト。</param>
        /// <param name="authenticationSchemeProvider"><see cref="IAuthenticationSchemeProvider"/> オブジェクト。</param>
        public AccountController(AccountUseCase useCase, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            UseCase = useCase;
            AuthenticationSchemeProvider = authenticationSchemeProvider;
        }
    }
}
