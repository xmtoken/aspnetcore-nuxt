using AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Metadata.Controllers
{
    /// <summary>
    /// メタデータに関する API を提供します。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class MetadataController : ControllerBase
    {
        /// <summary>
        /// <see cref="IMapper"/> オブジェクトを表します。
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// <see cref="IEntityMetadataProvider"/> オブジェクトを表します。
        /// </summary>
        private readonly IEntityMetadataProvider EntityMetadataProvider;

        /// <summary>
        /// <see cref="MetadataController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="mapper"><see cref="IMapper"/> オブジェクト。</param>
        /// <param name="entityMetadataProvider"><see cref="IEntityMetadataProvider"/> オブジェクト。</param>
        public MetadataController(IMapper mapper, IEntityMetadataProvider entityMetadataProvider)
        {
            Mapper = mapper;
            EntityMetadataProvider = entityMetadataProvider;
        }
    }
}
