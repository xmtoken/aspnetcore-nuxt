using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Extensions.CsvHelper.TypeConversion;
using CsvHelper.Configuration;
using System.Text;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
{
    //public class UserCsvModelClassMap : ClassMap<UserCsvModel>
    //{
    //    public UserCsvModelClassMap(Encoding encoding)
    //    {
    //        Map(x => x.Name)
    //            .TypeConverter(new FixedWidthConverter(encoding, length: 9));

    //        Map(x => x.ProfileEmailAddress)
    //            .TypeConverter(new FixedWidthConverter(encoding, length: 9));

    //        Map(x => x.ProfileBirthday)
    //            .TypeConverterOption.Format("yyyyMMdd")
    //            .TypeConverter(new FixedWidthConverter(encoding, length: 10));

    //        Map(x => x.ProfileGender)
    //            .ConvertUsing(model => model.ProfileGender switch
    //            {
    //                Gender.None => "なし",
    //                Gender.Male => "男性",
    //                Gender.Female => "女性",
    //                _ => null,
    //            })
    //            .TypeConverter(new FixedWidthConverter(encoding, length: 10));
    //    }
    //}
}
