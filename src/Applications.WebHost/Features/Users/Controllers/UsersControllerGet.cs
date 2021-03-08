using AspNetCoreNuxt.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
{
    public partial class UsersController
    {
        ///// <summary>
        ///// 指定された情報をもとにページングされたユーザー情報のコレクションを非同期に取得します。
        ///// </summary>
        ///// <param name="conditions">検索条件を表す <see cref="UserSearchConditions"/> オブジェクト。</param>
        ///// <param name="sort">ソート条件を表す <see cref="Sorting"/> オブジェクトのコレクション。</param>
        ///// <param name="paging">ページング条件を表す <see cref="Paging"/> オブジェクト。</param>
        ///// <param name="fields">取得する項目のプロパティを示す文字列のコレクション。</param>
        ///// <returns><see cref="IPagination{T}"/> オブジェクト。</returns>
        //[Authorize]
        //[HttpGet]
        //[SwaggerResponse(typeof(IPagination<User>))]
        //public async Task<object> Get([FromQuery] UserSearchConditions conditions, [FromQuery(Name = "sort")] IEnumerable<Sorting> sort, [FromQuery] Paging paging, [FromQuery] IEnumerable<string> fields)
        //{
        //    var specification = SearchSpecificationFactory.Create(conditions);
        //    var sortings = sort.ToQueryableOrDefault<User>((x => x.Id, SortDirection.Descending));


        //    var records = await UseCase.GetPaginatedUsersAsync(specification, sortings, paging, fields);
        //    var obj = new Pagination<object>()
        //    {
        //        CurrentPageIndex = records.CurrentPageIndex,
        //        CurrentPageItems = ExpandoObjectHelper.Convert(records.CurrentPageItems, fields),
        //        CurrentPageSize = records.CurrentPageSize,
        //        TotalItemCount = records.TotalItemCount,
        //        TotalPageCount = records.TotalPageCount,
        //    };


        //    //var records = await UseCase.GetUsersAsync(specification, sortings, fields);
        //    //var obj = ExpandoObjectHelper.Convert(records, fields);

        //    return obj;
        //}
    }


    public static class ExpandoObjectHelper
    {
        public static IEnumerable<object> Convert(IEnumerable source, IEnumerable<string> fields)
        {
            foreach (var item in source)
            {
                yield return ConvertIns(item, fields);
            }
        }

        public static object ConvertIns(object source, IEnumerable<string> fields)
        {

            //var comparer = StringComparer.OrdinalIgnoreCase;
            //var dictionary = new Dictionary<string, object>(comparer);
            var dictionary = new ExpandoObject() as IDictionary<string, object>;

            if (source is null)
            {
                return dictionary;
                //return dictionary as ExpandoObject;
            }

            foreach (var property in source.GetType().GetProperties())
            {
                if (fields.Any(field => field?.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
                    fields.Any(field => field?.StartsWith($"{property.Name}.", StringComparison.InvariantCultureIgnoreCase) ?? false))
                {
                    var key = property.Name;
                    var value = property.GetValue(source);

                    if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && !property.PropertyType.IsNullable())
                    {
                        var prefix = property.Name.ToLowerInvariant() + ".";
                        var nestfields = fields
                            .Where(x => !string.IsNullOrEmpty(x))
                            .Select(x => x.ToLowerInvariant())
                            .Where(x => x.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                            .Select(x => x.Remove(0, prefix.Length))
                            .ToArray();

                        value = ConvertIns(value, nestfields);
                    }


                    if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        if (value is IEnumerable enumerable)
                        {
                            var prefix = property.Name.ToLowerInvariant() + ".";
                            var nestfields = fields
                                .Where(x => !string.IsNullOrEmpty(x))
                                .Select(x => x.ToLowerInvariant())
                                .Where(x => x.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                                .Select(x => x.Remove(0, prefix.Length))
                                .ToArray();

                            value = enumerable.Cast<object>().Select(x => ConvertIns(x, nestfields));
                        }
                        else
                        {
                            value = Array.Empty<object>();
                        }
                    }

                    dictionary.Add(key, value);
                    continue;
                }
            }

            return dictionary;
            //return dictionary as ExpandoObject;
        }
    }
}
