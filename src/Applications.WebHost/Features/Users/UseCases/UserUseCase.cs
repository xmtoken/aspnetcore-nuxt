//using AspNetCoreNuxt.Domains.Data;
//using AspNetCoreNuxt.Extensions.DependencyInjection;
//using AspNetCoreNuxt.Extensions.Identity;
//using AutoMapper;
//using Microsoft.AspNetCore.Identity;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
//{
//    /// <summary>
//    /// ユーザーに関するユースケースを表します。
//    /// </summary>
//    public partial class UserUseCase : IDependencyInjectionService
//    {
//        /// <summary>
//        /// <see cref="AppDbContext"/> オブジェクトを表します。
//        /// </summary>
//        private readonly AppDbContext Context;

//        /// <summary>
//        /// <see cref="IMapper"/> オブジェクトを表します。
//        /// </summary>
//        private readonly IMapper Mapper;

//        /// <summary>
//        /// <see cref="ILookupNormalizer"/> オブジェクトを表します。
//        /// </summary>
//        private readonly ILookupNormalizer LookupNormalizer;

//        /// <summary>
//        /// <see cref="IStringHasher"/> オブジェクトを表します。
//        /// </summary>
//        private readonly IStringHasher Cryptography;

//        /// <summary>
//        /// <see cref="UserUseCase"/> クラスの新しいインスタンスを作成します。
//        /// </summary>
//        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
//        /// <param name="mapper"><see cref="IMapper"/> オブジェクト。</param>
//        /// <param name="lookupNormalizer"><see cref="ILookupNormalizer"/> オブジェクト。</param>
//        /// <param name="cryptography"><see cref="IStringHasher"/> オブジェクト。</param>
//        public UserUseCase(AppDbContext context, IMapper mapper, ILookupNormalizer lookupNormalizer, IStringHasher cryptography)
//        {
//            Context = context;
//            Mapper = mapper;
//            LookupNormalizer = lookupNormalizer;
//            Cryptography = cryptography;
//        }
//    }
//}
