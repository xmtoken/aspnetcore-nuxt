//using AspNetCoreNuxt.Domains;
//using AspNetCoreNuxt.Domains.Entities;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Seeds.Controllers
//{
//    public partial class SeedsController
//    {
//        /// <summary>
//        /// シードデータを非同期に登録します。
//        /// </summary>
//        /// <returns><see cref="Task"/> オブジェクト。</returns>
//        [HttpPost]
//        public async Task Post()
//        {
//            await Context.SaveChangesAsync();

//            await Context.Database.EnsureDeletedAsync();
//            await Context.Database.EnsureCreatedAsync();

//            for (int i = 1; i <= 100; i++)
//            {
//                Context.Set<UserEntity>().Add(new UserEntity()
//                {
//                    Name = i.ToString(),
//                    NormalizedName = LookupNormalizer.NormalizeName(i.ToString()),
//                    PasswordHash = Cryptography.HashValue(i.ToString()),
//                    Profile = new UserProfileEntity()
//                    {
//                        EmailAddress = $"{i}@gmail.com",
//                        NormalizedEmailAddress = LookupNormalizer.NormalizeEmail($"{i}@gmail.com"),
//                        Birthday = new DateTime(1991, 10, 1),
//                        Gender = (Gender)new Random().Next(0, 2),
//                    },
//                });
//            }

//            await Context.SaveChangesAsync();
//        }
//    }
//}
