using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.Collections.Generic;
using AutoMapper;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Roles.Profiles
{
    /// <summary>
    /// <see cref="RoleEntity"/> クラスから <see cref="TextValuePair{TValue}"/> クラスへのマッピングを提供します。
    /// </summary>
    public class RoleToTextValuePairProfile : Profile
    {
        /// <summary>
        /// <see cref="RoleToTextValuePairProfile"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public RoleToTextValuePairProfile()
        {
            CreateMap<RoleEntity, TextValuePair<int>>()
                .ForMember(x => x.Text, options => options.MapFrom(x => x.Name))
                .ForMember(x => x.Value, options => options.MapFrom(x => x.Id));
        }
    }
}
