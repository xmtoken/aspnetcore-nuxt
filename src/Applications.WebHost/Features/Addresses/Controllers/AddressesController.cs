using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        /// <see cref="ILogger"/> オブジェクトを表します。
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// <see cref="IMemoryCache"/> オブジェクトを表します。
        /// </summary>
        private readonly IMemoryCache MemoryCache;

        /// <summary>
        /// <see cref="AddressesController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="httpClientFactory"><see cref="IHttpClientFactory"/> オブジェクト。</param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/> オブジェクト。</param>
        /// <param name="memoryCache"><see cref="IMemoryCache"/> オブジェクト。</param>
        public AddressesController(IHttpClientFactory httpClientFactory, ILogger<AddressesController> logger, IMemoryCache memoryCache)
        {
            HttpClientFactory = httpClientFactory;
            Logger = logger;
            MemoryCache = memoryCache;
        }
    }
}
