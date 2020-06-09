using AspNetCoreNuxt.Applications.WebHost.Features.Users.Specifications;
using AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
{
    /// <summary>
    /// ユーザーに関する API を提供します。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        /// <summary>
        /// <see cref="UserUseCase"/> オブジェクトを表します。
        /// </summary>
        private readonly UserUseCase UseCase;

        /// <summary>
        /// <see cref="UserSearchSpecificationFactory"/> オブジェクトを表します。
        /// </summary>
        private readonly UserSearchSpecificationFactory SearchSpecificationFactory;

        /// <summary>
        /// <see cref="UsersController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="useCase"><see cref="UserUseCase"/> オブジェクト。</param>
        /// <param name="searchSpecificationFactory"><see cref="UserSearchSpecificationFactory"/> オブジェクト。</param>
        public UsersController(UserUseCase useCase, UserSearchSpecificationFactory searchSpecificationFactory)
        {
            UseCase = useCase;
            SearchSpecificationFactory = searchSpecificationFactory;
        }
    }
}
