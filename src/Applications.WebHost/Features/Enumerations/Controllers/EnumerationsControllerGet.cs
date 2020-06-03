using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Extensions.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Enumerations.Controllers
{
    public partial class EnumerationsController
    {
        /// <summary>
        /// 情報の取得をサポートする列挙体の型のコレクションを表します。
        /// </summary>
        private static readonly Type[] SupportedTypes = new[]
        {
            typeof(Gender),
            typeof(Permission),
        };

        /// <summary>
        /// 列挙体の表示名と値のコレクションを取得します。
        /// </summary>
        /// <returns><see cref="IDictionary{TKey, TValue}"/> オブジェクトのコレクション。</returns>
        [HttpGet]
        public IDictionary<string, object> Get()
        {
            var list = new Dictionary<string, object>();
            foreach (var type in SupportedTypes)
            {
                var provider = EnumLabelProviderFactory.CreateProvider(type);
                var item = Enum.GetValues(type).Cast<int>().Select(value => new TextValuePair<int>(provider.CreateLabel(value), value));
                list.Add(type.Name, item);
            }
            return list;
        }
    }
}
