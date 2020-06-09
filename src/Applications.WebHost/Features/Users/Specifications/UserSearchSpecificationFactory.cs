using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Specifications
{
    /// <summary>
    /// <see cref="UserSearchSpecification"/> オブジェクトを作成する機能を提供します。
    /// </summary>
    public class UserSearchSpecificationFactory : IApplicationService
    {
        /// <summary>
        /// <see cref="ILookupNormalizer"/> オブジェクトを表します。
        /// </summary>
        private readonly ILookupNormalizer LookupNormalizer;

        /// <summary>
        /// <see cref="UserSearchSpecificationFactory"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="lookupNormalizer"><see cref="ILookupNormalizer"/> オブジェクト。</param>
        public UserSearchSpecificationFactory(ILookupNormalizer lookupNormalizer)
        {
            LookupNormalizer = lookupNormalizer;
        }

        /// <summary>
        /// <see cref="UserSearchSpecification"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="conditions">検索条件を表す <see cref="IUserSearchConditions"/> オブジェクト。</param>
        /// <returns><see cref="UserSearchSpecification"/> オブジェクト。</returns>
        public UserSearchSpecification Create(IUserSearchConditions conditions)
            => new UserSearchSpecification(conditions, LookupNormalizer);
    }
}
