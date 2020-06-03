using AspNetCoreNuxt.Applications.WebHost.Features.Account.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Controllers
{
    public partial class AccountController
    {
        /// <summary>
        /// 認証されているアカウントの情報を取得します。
        /// </summary>
        /// <returns>アカウントの情報を表す <see cref="AccountIdentity"/> オブジェクト。</returns>
        [Authorize]
        [HttpGet]
        public AccountIdentity Get()
        {
            var userName = User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            var permissions = User.Claims.Where(x => x.Type == ClaimTypes.Role).ToDictionary(x => x.Value, _ => true);
            return new AccountIdentity(userName, permissions);
        }
    }
}
