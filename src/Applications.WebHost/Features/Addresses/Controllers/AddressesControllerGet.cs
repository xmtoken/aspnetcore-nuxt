using AspNetCoreNuxt.Applications.WebHost.Features.Addresses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Addresses.Controllers
{
    public partial class AddressesController
    {
        /// <summary>
        /// 指定された郵便番号に紐づく住所の情報を非同期に取得します。
        /// </summary>
        /// <param name="code">7 桁の郵便番号、もしくはハイフンを含む 8 桁の郵便番号。</param>
        /// <returns>住所の情報を表す <see cref="AddressModel"/> オブジェクトのコレクション。</returns>
        /// <remarks>郵便番号データ配信サービス zip-cloud は CORS が許可されていないため API アクセスをプロキシします。</remarks>
        [HttpGet]
        [Route("{code}")]
        public Task<IEnumerable<AddressModel>> Get([FromRoute] string code)
        {
            var zipcode = code?.Replace("-", null).Trim();
            var uri = $"https://zipcloud.ibsnet.co.jp/api/search?zipcode={zipcode}";
            return MemoryCache.GetOrCreateAsync(uri, async _ =>
            {
                using var client = HttpClientFactory.CreateClient();
                using var response = await client.GetAsync(new Uri(uri));
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
                return json.Results?.Select(x => new AddressModel()
                {
                    PostalCode = x.Zipcode,
                    Address = x.Address1 + x.Address2 + x.Address3,
                });
            });
        }
    }
}
