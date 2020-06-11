using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Addresses.Controllers
{
    /// <summary>
    /// 住所に関する API を提供します。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class AddressesController : ControllerBase
    {
        /// <summary>
        /// <see cref="IHttpClientFactory"/> オブジェクトを表します。
        /// </summary>
        private readonly IHttpClientFactory HttpClientFactory;

        /// <summary>
        /// <see cref="IMemoryCache"/> オブジェクトを表します。
        /// </summary>
        private readonly IMemoryCache MemoryCache;

        /// <summary>
        /// <see cref="AddressesController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="httpClientFactory"><see cref="IHttpClientFactory"/> オブジェクト。</param>
        /// <param name="memoryCache"><see cref="IMemoryCache"/> オブジェクト。</param>
        public AddressesController(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            HttpClientFactory = httpClientFactory;
            MemoryCache = memoryCache;
        }
    }
}
