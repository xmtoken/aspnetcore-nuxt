using AspNetCoreNuxt.Applications.WebHost.Core.Entities;
using AspNetCoreNuxt.Applications.WebHost.Features.Metadata.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Metadata.Controllers
{
    public partial class MetadataController
    {
        /// <summary>
        /// エンティティのメタデータを取得します。
        /// </summary>
        /// <returns><see cref="IDictionary{TKey, TValue}"/> オブジェクト。</returns>
        [HttpGet]
        public IDictionary<string, object> Get()
        {
            var entities = new Dictionary<string, object>();
            foreach (var map in Mapper.ConfigurationProvider.GetAllTypeMaps())
            {
                var entityInterfaces = map.DestinationType.GetInterfaces();
                var entityInterface = entityInterfaces.SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntity<>));
                if (entityInterface == null || entityInterface.GenericTypeArguments[0] != map.SourceType)
                {
                    continue;
                }

                var entityMetadata = EntityMetadataProvider.Entity(map.SourceType);
                var entityProperties = new Dictionary<string, object>();
                foreach (var memberMap in map.MemberMaps.Where(x => x.SourceMember != null))
                {
                    var propertyMetadata = entityMetadata.FindProperty(memberMap.SourceMember.Name);
                    if (propertyMetadata == null)
                    {
                        continue;
                    }

                    entityProperties.Add(memberMap.DestinationName, new EntityPropertyMetadata()
                    {
                        MaxLength = propertyMetadata.GetMaxLength(),
                    });
                }

                entities.Add(map.DestinationType.Name, entityProperties);
            }

            return entities;
        }
    }
}
