using AspNetCoreNuxt.Applications.WebHost.Features.Account.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Controllers
{
    public partial class AccountController
    {
        /// <summary>
        /// 指定された認証情報をもとに認証チケットを発行します。
        /// </summary>
        /// <param name="credentials">認証情報を表す <see cref="Credentials"/> オブジェクト。</param>
        /// <returns><see cref="SignInResult"/> オブジェクト。</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<SignInResult> SignIn([FromBody] Credentials credentials)
        {
            var permissions = await UseCase.GetPermissionsAsync(credentials.UserName);

            var claims = permissions
                .Select(permission => new Claim(ClaimTypes.Role, permission.ToString()))
                .Append(new Claim(ClaimTypes.Name, credentials.UserName));
            var scheme = await AuthenticationSchemeProvider.GetDefaultAuthenticateSchemeAsync();
            var identity = new ClaimsIdentity(claims, scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,
            };

            return SignIn(principal, properties, scheme.Name);
        }
    }
}
