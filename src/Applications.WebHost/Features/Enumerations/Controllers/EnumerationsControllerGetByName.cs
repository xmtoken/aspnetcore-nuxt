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
        /// 指定された名前の列挙体の表示名と値のコレクションを取得します。
        /// </summary>
        /// <param name="name">列挙体の名前。</param>
        /// <returns><see cref="TextValuePair{TValue}"/> オブジェクトのコレクション。</returns>
        [HttpGet]
        [Route("{name}")]
        public IEnumerable<TextValuePair<int>> Get(string name)
            => (IEnumerable<TextValuePair<int>>)Get().SingleOrDefault(x => x.Key.Equals(name, StringComparison.OrdinalIgnoreCase)).Value;
    }
}
