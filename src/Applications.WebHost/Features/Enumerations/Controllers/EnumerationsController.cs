using AspNetCoreNuxt.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Enumerations.Controllers
{
    /// <summary>
    /// 列挙体に関する API を提供します。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class EnumerationsController : ControllerBase
    {
        /// <summary>
        /// <see cref="IEnumLabelProviderFactory"/> オブジェクトを表します。
        /// </summary>
        private readonly IEnumLabelProviderFactory EnumLabelProviderFactory;

        /// <summary>
        /// <see cref="EnumerationsController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="enumLabelProviderFactory"><see cref="IEnumLabelProviderFactory"/> オブジェクト。</param>
        public EnumerationsController(IEnumLabelProviderFactory enumLabelProviderFactory)
        {
            EnumLabelProviderFactory = enumLabelProviderFactory;
        }
    }
}
