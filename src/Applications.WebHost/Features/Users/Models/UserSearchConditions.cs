using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
{
    /// <inheritdoc cref="IUserSearchConditions"/>
    public class UserSearchConditions : IUserSearchConditions
    {
        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        [FromQuery(Name = "name")]
        public string UserName { get; set; }

        /// <summary>
        /// メールアドレスを取得または設定します。
        /// </summary>
        [FromQuery(Name = "email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 生年月日を取得または設定します。
        /// </summary>
        [FromQuery(Name = "birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// 性別のコレクションを取得または設定します。
        /// </summary>
        [FromQuery(Name = "gender")]
        public IEnumerable<string> Genders { get; set; }

        /// <summary>
        /// ロールのコレクションを取得または設定します。
        /// </summary>
        [FromQuery(Name = "role")]
        public IEnumerable<string> Roles { get; set; }

        /// <inheritdoc/>
        DateTime? IUserSearchConditions.Birthday
            => QueryStringConverter.ConvertToDateTime(Birthday);

        /// <inheritdoc/>
        IEnumerable<Gender> IUserSearchConditions.Genders
            => QueryStringConverter.ConvertToEnum<Gender>(Genders);

        /// <inheritdoc/>
        IEnumerable<int> IUserSearchConditions.Roles
            => QueryStringConverter.ConvertToInt32(Roles);
    }
}
