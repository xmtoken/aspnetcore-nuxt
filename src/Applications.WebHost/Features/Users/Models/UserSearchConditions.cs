using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Extensions;
using AspNetCoreNuxt.Extensions.AspNetCore;
using AspNetCoreNuxt.Extensions.NSwag.Generation.Processors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
{
    /// <inheritdoc cref="IUserSearchConditions"/>
    public class UserSearchConditions : IOpenApiSchema, IUserSearchConditions
    {
        ///// <summary>
        ///// ユーザー名を取得します。
        ///// </summary>
        //[FromQuery(Name = "name")]
        //public string UserName { get; private set; }

        ///// <summary>
        ///// メールアドレスを取得します。
        ///// </summary>
        //[FromQuery(Name = "email")]
        //public string EmailAddress { get; private set; }

        ///// <summary>
        ///// 生年月日を取得します。
        ///// </summary>
        //[FromQuery(Name = "birthday")]
        //public string Birthday { get; private set; }

        ///// <summary>
        ///// 性別のコレクションを取得します。
        ///// </summary>
        //[FromQuery(Name = "gender")]
        //public IEnumerable<string> Genders { get; private set; }

        ///// <summary>
        ///// ロールのコレクションを取得します。
        ///// </summary>
        //[FromQuery(Name = "role")]
        //public IEnumerable<string> Roles { get; private set; }

        ///// <inheritdoc/>
        //DateTime? IUserSearchConditions.Birthday
        //    => QueryStringConverter.ConvertToDateTime(Birthday);

        ///// <inheritdoc/>
        //IEnumerable<Gender> IUserSearchConditions.Genders
        //    => QueryStringConverter.ConvertToEnum<Gender>(Genders);

        ///// <inheritdoc/>
        //IEnumerable<int> IUserSearchConditions.Roles
        //    => QueryStringConverter.ConvertToInt32(Roles);



        //public string UserId { get; set; }

        //int? IUserSearchConditions.UserId
        //    => QueryStringConverter.ConvertToInt32(UserId);

        //public string UserIdComparison { get; set; }

        //ValueComparisonOperator? IUserSearchConditions.UserIdComparison
        //    => QueryStringConverter.ConvertToEnum<ValueComparisonOperator>(UserIdComparison);
    }
}
