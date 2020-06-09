using AspNetCoreNuxt.Applications.WebHost.Core.Validators;
using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.AspNetCore.Routing;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata;
using FluentValidation;
using FluentValidation.Resources;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Validators
{
    /// <summary>
    /// <see cref="UserUpdateModel"/> クラスの検証ロジックを提供します。
    /// </summary>
    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        /// <summary>
        /// <see cref="UserUpdateModelValidator"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        /// <param name="httpContextAccessor"><see cref="IHttpContextAccessor"/> オブジェクト。</param>
        /// <param name="metadata"><see cref="IEntityMetadataProvider"/> オブジェクト。</param>
        public UserUpdateModelValidator(AppDbContext context, IHttpContextAccessor httpContextAccessor, IEntityMetadataProvider metadata)
        {
            var id = httpContextAccessor.HttpContext.Request.RouteValues.GetValue<int>("id");

            RuleFor(x => x)
                .ChildRules(validator =>
                {
                    validator
                        .RuleFor(x => x)
                        .CustomAsync(async (model, validationContext, cancellationToken) =>
                        {
                            var user = await context.Set<UserEntity>().FindByIdAsync(id, QueryTrackingBehavior.TrackAll);
                            if (user == null)
                            {
                                validationContext.AddFailure("ユーザー情報はすでに削除されているため更新できませんでした。");
                            }
                        });
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.EmailAddress)
                        .Configure(x => x.DisplayName = new StaticStringSource("メールアドレス"))
                        .NotNull()
                        .MaximumLength(metadata.Entity<UserProfileEntity>().Property(x => x.EmailAddress).GetMaxLength().Value)
                        .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

                    RuleFor(x => x.Birthday)
                        .Configure(x => x.DisplayName = new StaticStringSource("生年月日"))
                        .NotNull()
                        .Birthday();

                    RuleFor(x => x.Gender)
                        .Configure(x => x.DisplayName = new StaticStringSource("性別"))
                        .NotNull()
                        .IsInEnum();

                    RuleFor(x => x.Roles)
                        .Configure(x => x.DisplayName = new StaticStringSource("ロール"))
                        .Required()
                        .CustomAsync(async (roles, validationContext, cancellationToken) =>
                        {
                            var roleIds = await context.Set<RoleEntity>().Select(x => x.Id).ToArrayAsync();
                            if (roles.Any(x => !roleIds.Contains(x)))
                            {
                                validationContext.AddFailure($"{validationContext.DisplayName}に無効な値が含まれています。");
                            }
                        });
                });
        }
    }
}
