using AspNetCoreNuxt.Domains.Entities._Examples;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Domains.Data.QueryableExtensions._Examples
{
    public static class CustomerEntityQueryableExtensions
    {
        //public static IQueryable<CustomerEntity> LeftJoin(this IQueryable<CustomerEntity> source, DbSet<OneEntity> xxx)
        //{
        //    return source.SelectWith(x => new()
        //    {
        //        Ones = xxx
        //            .Where(one => one.Id > x.Id && one.Id < 99)
        //            .AsEnumerable(),
        //    });
        //}

        public static IQueryable<CustomerEntity> LeftJoin(this IQueryable<CustomerEntity> source, DbSet<ManyEntity> xxx)
        {
            return source.SelectWith(x => new()
            {
                Many = xxx
                    .Where(one => one.Id > x.Id && one.Id < 99)
                    .ToList(),
            });
        }
    }
}
