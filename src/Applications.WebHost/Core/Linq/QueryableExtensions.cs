using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Linq
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class QueryableExtensions
    {
        public static IQueryable<T> TryAddPagingQuery<T>(this IQueryable<T> source, IPagingQuery paging)
        {
            var linq = source;

            if (paging?.Offset is int offset && offset >= 0)
            {
                linq = linq.Skip(offset);
            }

            if (paging?.Limit is int limit && limit >= 0)
            {
                linq = linq.Take(limit);
            }

            return linq;
        }

        public static IQueryable<T> TryAddProjectionQuery<T>(this IQueryable<T> source, IMapper mapper, IFieldQuery<T> field)
        {
            var linq = source.AsQueryable();

            if (field is not null)
            {
                var fieldNames = field.PropertyNames.ToArray();
                linq = source.ProjectTo<T>(mapper.ConfigurationProvider, parameters: null, fieldNames);
            }

            return linq;
        }

        public static IQueryable<T> TryAddSpecificationQuery<T>(this IQueryable<T> source, SearchSpecification<T> specification)
        {
            var linq = source.AsQueryable();

            if (specification is not null)
            {
                linq = linq.Where(specification.GetExpression());
            }

            return linq;
        }

        public static IQueryable<T> TryAddSortingQuery<T>(this IQueryable<T> source, ISortQuery<T> sorting)
        {
            var linq = source.AsQueryable();

            //if (sorting?.GetSortFunction<T>() is Func<IQueryable<T>, IQueryable<T>> sorter)
            //{
            //    linq = sorter(linq);
            //}

            return linq;
        }
    }
}
