using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Specifications
{
    /// <summary>
    /// ユーザーを検索する仕様を表します。
    /// </summary>
    public class UserSearchSpecification : LinqSpecification<UserEntity>
    {
        /// <summary>
        /// <see cref="IUserSearchConditions"/> オブジェクトを表します。
        /// </summary>
        private readonly IUserSearchConditions Conditions;

        /// <summary>
        /// <see cref="ILookupNormalizer"/> オブジェクトを表します。
        /// </summary>
        private readonly ILookupNormalizer LookupNormalizer;

        /// <summary>
        /// <see cref="UserSearchSpecification"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="conditions">検索条件を表す <see cref="IUserSearchConditions"/> オブジェクト。</param>
        /// <param name="lookupNormalizer"><see cref="ILookupNormalizer"/> オブジェクト。</param>
        public UserSearchSpecification(IUserSearchConditions conditions, ILookupNormalizer lookupNormalizer)
        {
            Conditions = conditions;
            LookupNormalizer = lookupNormalizer;
        }

        /// <inheritdoc/>
        protected override IEnumerable<Expression<Func<UserEntity, bool>>> GetExpressions()
        {
            if (Conditions.UserName != null)
            {
                var normalizedName = LookupNormalizer.NormalizeName(Conditions.UserName);
                yield return x => x.NormalizedName.StartsWith(normalizedName);
            }

            if (Conditions.EmailAddress != null)
            {
                var normalizedEmailAddress = LookupNormalizer.NormalizeEmail(Conditions.EmailAddress);
                yield return x => x.Profile.NormalizedEmailAddress.StartsWith(normalizedEmailAddress);
            }

            if (Conditions.Birthday != null)
            {
                yield return x => x.Profile.Birthday == Conditions.Birthday;
            }

            if (Conditions.Genders != null && Conditions.Genders.Any())
            {
                yield return x => Conditions.Genders.Contains(x.Profile.Gender);
            }

            if (Conditions.Roles != null && Conditions.Roles.Any())
            {
                yield return x => x.Roles.Any(role => Conditions.Roles.Contains(role.RoleId));
            }
        }
    }
}
