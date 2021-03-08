//using AspNetCoreNuxt.Applications.WebHost.Core.Models;
//using AspNetCoreNuxt.Applications.WebHost.Features.Users.Specifications;
//using AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases;
//using AspNetCoreNuxt.Extensions.Collections;
//using AspNetCoreNuxt.Extensions.CsvHelper.Configuration;
//using AspNetCoreNuxt.Extensions.DependencyInjection;
//using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
//using CsvHelper;
//using CsvHelper.Configuration;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
//{
//    public class UserCsvWriter : IDependencyInjectionService
//    {
//        /// <summary>
//        /// <see cref="UserUseCase"/> オブジェクトを表します。
//        /// </summary>
//        private readonly UserUseCase UseCase;

//        /// <summary>
//        /// <see cref="UserSearchSpecificationFactory"/> オブジェクトを表します。
//        /// </summary>
//        private readonly UserSearchSpecificationFactory SearchSpecificationFactory;

//        /// <summary>
//        /// <see cref="UserCsvWriter"/> クラスの新しいインスタンスを作成します。
//        /// </summary>
//        /// <param name="useCase"><see cref="UserUseCase"/> オブジェクト。</param>
//        /// <param name="searchSpecificationFactory"><see cref="UserSearchSpecificationFactory"/> オブジェクト。</param>
//        public UserCsvWriter(UserUseCase useCase, UserSearchSpecificationFactory searchSpecificationFactory)
//        {
//            UseCase = useCase;
//            SearchSpecificationFactory = searchSpecificationFactory;
//        }

//        //public async Task<byte[]> GetUserCsvByteArrayAsync(UserSearchConditions conditions, IEnumerable<Sorting> sort, Encoding encoding)
//        //{
//        //    using var memory = new MemoryStream();
//        //    using var stream = new StreamWriter(memory, encoding);
//        //    using var writer = new CsvWriter(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
//        //    {
//        //        Encoding = encoding,
//        //    });

//        //    //writer.Configuration.HasHeaderRecord = true;
//        //    //writer.Configuration.ShouldQuote = (field, context) => true;

//        //    var map = new UserCsvModelClassMap(encoding);
//        //    writer.Configuration.Delimiter = null;
//        //    writer.Configuration.HasHeaderRecord = false;
//        //    writer.Configuration.ShouldQuote = (field, context) => false;
//        //    writer.Configuration.RegisterClassMap(map);

//        //    //var specification = SearchSpecificationFactory.Create(conditions);
//        //    //var sortings = sort.ToQueryableOrDefault<UserCsvModel>((x => x.Name, SortDirection.Descending));
//        //    //var fields = map.GetMapMemberExpressions().ToArray();
//        //    //var records = await UseCase.GetUsersAsync(specification, sortings, fields);

//        //    ////writer.WriteHeader<UserCsvModel>();
//        //    ////writer.NextRecord();
//        //    //await writer.WriteRecordsAsync(records);
//        //    //await stream.FlushAsync();

//        //    //return memory.ToArray();

//        //    return null;
//        //}
//    }
//}
