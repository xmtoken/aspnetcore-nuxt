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
        /// <returns>郵便番号に紐づく住所。</returns>
        /// <remarks>郵便番号データ配信サービス zip-cloud は CORS が許可されていないため API アクセスをプロキシします。</remarks>
        [HttpGet]
        public async Task<JsonResult> Get([FromQuery] string code)
        {
            using var client = HttpClientFactory.CreateClient();
            using var response = await client.GetAsync(new Uri($"https://zipcloud.ibsnet.co.jp/api/search?zipcode={code}"));
            var content = await response.Content.ReadAsStringAsync();

            // http://zipcloud.ibsnet.co.jp/doc/api
            var anonymouseType = new
            {
                Status = default(int),
                Message = default(string),
                Results = new[]
                {
                    new
                    {
                        Zipcode = default(string),
                        PrefCode = default(string),
                        Address1 = default(string),
                        Address2 = default(string),
                        Address3 = default(string),
                        Kana1 = default(string),
                        Kana2 = default(string),
                        Kana3 = default(string),
                    },
                },
            };
            var json = JsonConvert.DeserializeAnonymousType(content, anonymouseType);
            return new JsonResult(json);
        }
    }
}
