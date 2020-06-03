using AspNetCoreNuxt.Domains.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Initializations.Controllers
{
    /// <summary>
    /// 初期化に関する API を提供します。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class InitializationsController : ControllerBase
    {
        /// <summary>
        /// <see cref="AppDbContext"/> オブジェクトを表します。
        /// </summary>
        private readonly AppDbContext Context;

        /// <summary>
        /// <see cref="InitializationsController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        public InitializationsController(AppDbContext context)
        {
            Context = context;
        }
    }
}
