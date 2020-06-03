using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Metadata.Controllers
{
    public partial class MetadataController
    {
        /// <summary>
        /// 指定された名前のエンティティのメタデータを取得します。
        /// </summary>
        /// <param name="name">エンティティの名前。</param>
        /// <returns><see cref="IDictionary{TKey, TValue}"/> オブジェクト。</returns>
        [HttpGet]
        [Route("{name}")]
        public IDictionary<string, object> Get(string name)
            => (IDictionary<string, object>)Get().SingleOrDefault(x => x.Key.Equals(name, StringComparison.OrdinalIgnoreCase)).Value;
    }
}
