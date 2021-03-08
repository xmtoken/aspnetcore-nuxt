//using AspNetCoreNuxt.Domains.Data;
//using AspNetCoreNuxt.Extensions.Identity;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Seeds.Controllers
//{
//    /// <summary>
//    /// シードデータに関する API を提供します。
//    /// </summary>
//    [ApiController]
//    [Route("api/[controller]")]
//    public partial class SeedsController : ControllerBase
//    {
//        /// <summary>
//        /// <see cref="AppDbContext"/> オブジェクトを表します。
//        /// </summary>
//        private readonly AppDbContext Context;

//        /// <summary>
//        /// <see cref="ILookupNormalizer"/> オブジェクトを表します。
//        /// </summary>
//        private readonly ILookupNormalizer LookupNormalizer;

//        /// <summary>
//        /// <see cref="IStringHasher"/> オブジェクトを表します。
//        /// </summary>
//        private readonly IStringHasher Cryptography;

//        /// <summary>
//        /// <see cref="SeedsController"/> クラスの新しいインスタンスを作成します。
//        /// </summary>
//        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
//        /// <param name="lookupNormalizer"><see cref="ILookupNormalizer"/> オブジェクト。</param>
//        /// <param name="cryptography"><see cref="IStringHasher"/> オブジェクト。</param>
//        public SeedsController(AppDbContext context, ILookupNormalizer lookupNormalizer, IStringHasher cryptography)
//        {
//            Context = context;
//            LookupNormalizer = lookupNormalizer;
//            Cryptography = cryptography;
//        }
//    }
//}
