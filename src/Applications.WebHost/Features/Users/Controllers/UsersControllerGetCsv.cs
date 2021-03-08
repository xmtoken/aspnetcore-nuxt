using AspNetCoreNuxt.Applications.WebHost.Core.Models;
using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Extensions.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
{
    public partial class UsersController
    {
        //[Authorize]
        //[HttpGet]
        //[Route("[action]")]
        //public async Task<IActionResult> Csv([FromQuery] UserSearchConditions conditions, [FromQuery(Name = "sort")] IEnumerable<Sorting> sort)
        //{
        //    var bytes = await CsvWriter.GetUserCsvByteArrayAsync(conditions, sort, Encoding.GetEncoding("Shift-JIS"));
        //    var fileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.csv";
        //    return File(bytes, ContentTypeProvider.GetContentType(fileName), fileName);
        //}
    }
}
