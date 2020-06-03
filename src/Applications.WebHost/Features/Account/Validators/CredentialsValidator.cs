using AspNetCoreNuxt.Applications.WebHost.Features.Account.Models;
using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.Identity;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Validators
{
    /// <summary>
    /// <see cref="Credentials"/> クラスの検証ロジックを提供します。
    /// </summary>
    public class CredentialsValidator : AbstractValidator<Credentials>
    {
        /// <summary>
        /// <see cref="CredentialsValidator"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><see cref="AppDbContext"/> オブジェクト。</param>
        /// <param name="cryptography"><see cref="IStringHasher"/> オブジェクト。</param>
        public CredentialsValidator(AppDbContext context, IStringHasher cryptography)
        {
            RuleFor(x => x)
                .ChildRules(validator =>
                {
                    validator
                        .RuleFor(x => x.UserName)
                        .Configure(x => x.DisplayName = new StaticStringSource("ユーザー名"))
                        .NotNull();

                    validator
                        .RuleFor(x => x.Password)
                        .Configure(x => x.DisplayName = new StaticStringSource("パスワード"))
                        .NotNull();
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .CustomAsync(async (credentials, validationContext, cancellationToken) =>
                        {
                            var user = await context.Set<UserEntity>().FindByNameAsync(credentials.UserName, QueryTrackingBehavior.NoTracking);
                            if (cryptography.VerifyHashedValue(user?.PasswordHash, credentials.Password))
                            {
                                return;
                            }
                            validationContext.AddFailure("ユーザー名もしくはパスワードが正しくありません。");
                        });
                });
        }
    }
}
