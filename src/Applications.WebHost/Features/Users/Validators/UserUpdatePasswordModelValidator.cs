using AspNetCoreNuxt.Applications.WebHost.Core.Validators;
using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.AspNetCore.Routing;
using AspNetCoreNuxt.Extensions.Identity;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Validators
{
    /// <summary>
    /// <see cref="UserUpdatePasswordModel"/> クラスの検証ロジックを提供します。
    /// </summary>
    public class UserUpdatePasswordModelValidator : AbstractValidator<UserUpdatePasswordModel>
    {
        /// <summary>
        /// <see cref="UserUpdatePasswordModelValidator"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        /// <param name="httpContextAccessor"><see cref="IHttpContextAccessor"/> オブジェクト。</param>
        /// <param name="cryptography"><see cref="IStringHasher"/> オブジェクト。</param>
        public UserUpdatePasswordModelValidator(AppDbContext context, IHttpContextAccessor httpContextAccessor, IStringHasher cryptography)
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
                    RuleFor(x => x.CurrentPassword)
                        .Configure(x => x.DisplayName = new StaticStringSource("現在のパスワード"))
                        .NotNull()
                        .CustomAsync(async (password, validationContext, cancellationToken) =>
                        {
                            var user = await context.Set<UserEntity>().FindByIdAsync(id, QueryTrackingBehavior.TrackAll);
                            if (cryptography.VerifyHashedValue(user?.PasswordHash, password))
                            {
                                return;
                            }
                            validationContext.AddFailure($"{validationContext.DisplayName}が正しくありません。");
                        });

                    RuleFor(x => x.NewPassword)
                        .Configure(x => x.DisplayName = new StaticStringSource("新しいパスワード"))
                        .NotNull()
                        .Password();
                });
        }
    }
}
