//using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
//using AspNetCoreNuxt.Applications.WebHost.Features.Users.Specifications;
//using AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.StaticFiles;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
//{
//    /// <summary>
//    /// ユーザーに関する API を提供します。
//    /// </summary>
//    [ApiController]
//    [Route("api/[controller]")]
//    public partial class UsersController : ControllerBase
//    {
//        /// <summary>
//        /// <see cref="UserUseCase"/> オブジェクトを表します。
//        /// </summary>
//        private readonly UserUseCase UseCase;

//        /// <summary>
//        /// <see cref="UserSearchSpecificationFactory"/> オブジェクトを表します。
//        /// </summary>
//        private readonly UserSearchSpecificationFactory SearchSpecificationFactory;

//        /// <summary>
//        /// <see cref="UserCsvWriter"/> オブジェクトを表します。
//        /// </summary>
//        private readonly UserCsvWriter CsvWriter;


//        /// <summary>
//        /// <see cref="IContentTypeProvider"/> オブジェクトを表します。
//        /// </summary>
//        private readonly IContentTypeProvider ContentTypeProvider;

//        /// <summary>
//        /// <see cref="UsersController"/> クラスの新しいインスタンスを作成します。
//        /// </summary>
//        /// <param name="useCase"><see cref="UserUseCase"/> オブジェクト。</param>
//        /// <param name="searchSpecificationFactory"><see cref="UserSearchSpecificationFactory"/> オブジェクト。</param>
//        /// <param name="csvWriter"><see cref="UserCsvWriter"/> オブジェクト。</param>
//        /// <param name="contentTypeProvider"><see cref="IContentTypeProvider"/> オブジェクト。</param>
//        public UsersController(UserUseCase useCase, UserSearchSpecificationFactory searchSpecificationFactory, UserCsvWriter csvWriter, IContentTypeProvider contentTypeProvider)
//        {
//            UseCase = useCase;
//            SearchSpecificationFactory = searchSpecificationFactory;
//            CsvWriter = csvWriter;
//            ContentTypeProvider = contentTypeProvider;
//        }
//    }
//}
