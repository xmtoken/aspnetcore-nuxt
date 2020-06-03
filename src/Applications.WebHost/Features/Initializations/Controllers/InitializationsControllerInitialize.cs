using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Initializations.Controllers
{
    public partial class InitializationsController
    {
        /// <summary>
        /// アプリケーションの初期処理を実行します。
        /// </summary>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        [HttpGet]
        [Route("[action]")]
        public Task Initialize()
        {
            _ = Context.Model;
            return Task.CompletedTask;
        }
    }
}
