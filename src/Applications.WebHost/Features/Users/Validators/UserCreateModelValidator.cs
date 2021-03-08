using AspNetCoreNuxt.Applications.WebHost.Core.Validators;
using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Validators
{
    ///// <summary>
    ///// <see cref="UserCreateModel"/> クラスの検証ロジックを提供します。
    ///// </summary>
    //public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    //{
    //    /// <summary>
    //    /// <see cref="UserCreateModelValidator"/> クラスの新しいインスタンスを作成します。
    //    /// </summary>
    //    /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
    //    /// <param name="entityMetadataProvider"><see cref="IEntityMetadataProvider{TContext}"/> オブジェクト。</param>
    //    public UserCreateModelValidator(AppDbContext context, IEntityMetadataProvider<AppDbContext> entityMetadataProvider)
    //    {
    //        var userEntityMetadata = entityMetadataProvider.Entity<User>();
    //        var userProfileEntityMetadata = entityMetadataProvider.Entity<UserProfile>();

    //        RuleFor(x => x.UserName)
    //            .Configure(x => x.DisplayName = new StaticStringSource("ユーザー名"))
    //            .SetValidatorByMetadata(userEntityMetadata)
    //            .UserName()
    //            .CustomAsync(async (userName, validationContext, cancellationToken) =>
    //            {
    //                var user = await context.Set<UserEntity>().FindByNameAsync(userName, QueryTrackingBehavior.TrackAll);
    //                if (user != null)
    //                {
    //                    validationContext.AddFailure($"すでに登録されている{validationContext.DisplayName}は使用できません。");
    //                }
    //            });

    //        RuleFor(x => x.Password)
    //            .Configure(x => x.DisplayName = new StaticStringSource("パスワード"))
    //            .SetValidatorByMetadata(userEntityMetadata)
    //            .Password();

    //        RuleFor(x => x.EmailAddress)
    //            .Configure(x => x.DisplayName = new StaticStringSource("メールアドレス"))
    //            .SetValidatorByMetadata(userProfileEntityMetadata)
    //            .EmailAddress();

    //        RuleFor(x => x.Birthday)
    //            .Configure(x => x.DisplayName = new StaticStringSource("生年月日"))
    //            .SetValidatorByMetadata(userProfileEntityMetadata)
    //            .Birthday();

    //        RuleFor(x => x.Gender)
    //            .Configure(x => x.DisplayName = new StaticStringSource("性別"))
    //            .SetValidatorByMetadata(userProfileEntityMetadata);

    //        RuleFor(x => x.Roles)
    //            .Configure(x => x.DisplayName = new StaticStringSource("ロール"))
    //            .Required()
    //            .CustomAsync(async (roles, validationContext, cancellationToken) =>
    //            {
    //                var roleIds = await context.Set<RoleEntity>().Select(x => x.Id).ToArrayAsync(cancellationToken);
    //                if (roles.Any(x => !roleIds.Contains(x)))
    //                {
    //                    validationContext.AddFailure($"{validationContext.DisplayName}に無効な値が含まれています。");
    //                }
    //            });
    //    }
    //}
}
