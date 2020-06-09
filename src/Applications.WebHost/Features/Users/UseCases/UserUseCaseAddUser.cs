using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
{
    public partial class UserUseCase
    {
        /// <summary>
        /// 指定された情報をもとにユーザー情報を非同期に登録します。
        /// </summary>
        /// <param name="model">ユーザーの登録情報を表す <see cref="UserCreateModel"/> オブジェクト。</param>
        /// <returns>ユーザー情報の登録に成功したかどうかを表す値と、登録したユーザー情報に割り当てられたユーザー ID。</returns>
        public async Task<(bool, int)> AddUserAsync(UserCreateModel model)
        {
            var entity = new UserEntity()
            {
                Name = model.UserName,
                NormalizedName = LookupNormalizer.NormalizeName(model.UserName),
                PasswordHash = Cryptography.HashValue(model.Password),
                Profile = new UserProfileEntity()
                {
                    EmailAddress = model.EmailAddress,
                    NormalizedEmailAddress = LookupNormalizer.NormalizeEmail(model.EmailAddress),
                    Birthday = model.Birthday.Value,
                    Gender = model.Gender.Value,
                },
                Roles = model.Roles?.Select(roleId => new UserRoleEntity()
                {
                    RoleId = roleId,
                }).ToArray(),
            };

            await Context.Set<UserEntity>().AddAsync(entity);
            return await Policy<(bool, int)>
                .Handle<DbUpdateException>(ex => ex.IsUniqueConstraintViolation())
                .FallbackAsync((false, default))
                .ExecuteAsync(async () => (await Context.SaveChangesAsync() != 0, entity.Id));
        }
    }
}
