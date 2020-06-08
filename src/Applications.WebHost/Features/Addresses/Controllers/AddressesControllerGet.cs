using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Addresses.Controllers
{
    public partial class AddressesController
    {
        /// <summary>
        /// 指定された郵便番号に紐づく住所を非同期に取得します。
        /// </summary>
        /// <param name="code">郵便番号。</param>
        /// <returns>郵便番号データ配信サービス zip-cloud は CORS が許可されていないため API アクセスをプロキシします。</returns>
        [HttpGet]
        public async Task<JsonResult> Get([FromQuery] string code)
        {
            using var client = HttpClientFactory.CreateClient();
            using var response = await client.GetAsync(new Uri($"https://zipcloud.ibsnet.co.jp/api/search?zipcode={code}"));
            var content = await response.Content.ReadAsStringAsync();

            // http://zipcloud.ibsnet.co.jp/doc/api
            var anonymouseType = new
            {
                Status = 0,
                Message = string.Empty,
                Results = new[]
                {
                    new
                    {
                        Zipcode = string.Empty,
                        PrefCode = string.Empty,
                        Address1 = string.Empty,
                        Address2 = string.Empty,
                        Address3 = string.Empty,
                        Kana1 = string.Empty,
                        Kana2 = string.Empty,
                        Kana3 = string.Empty,
                    },
                },
            };
            var json = JsonConvert.DeserializeAnonymousType(content, anonymouseType);
            return new JsonResult(json);
        }
    }
}
