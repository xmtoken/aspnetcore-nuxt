using AspNetCoreNuxt.Applications.WebHost.Features.Roles.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Roles.Controllers
{
    /// <summary>
    /// ロールに関する API を提供します。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class RolesController : ControllerBase
    {
        /// <summary>
        /// <see cref="RolesUseCase"/> オブジェクトを表します。
        /// </summary>
        private readonly RolesUseCase UseCase;

        /// <summary>
        /// <see cref="RolesController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="useCase"><see cref="RolesUseCase"/> オブジェクト。</param>
        public RolesController(RolesUseCase useCase)
        {
            UseCase = useCase;
        }
    }
}
