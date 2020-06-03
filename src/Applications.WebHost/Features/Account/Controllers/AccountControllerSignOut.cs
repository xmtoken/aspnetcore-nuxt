using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Controllers
{
    public partial class AccountController
    {
        /// <summary>
        /// 認証チケットを破棄します。
        /// </summary>
        /// <returns><see cref="SignOutResult"/> オブジェクト。</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<SignOutResult> SignOut()
        {
            var scheme = await AuthenticationSchemeProvider.GetDefaultAuthenticateSchemeAsync();
            return SignOut(scheme.Name);
        }
    }
}
