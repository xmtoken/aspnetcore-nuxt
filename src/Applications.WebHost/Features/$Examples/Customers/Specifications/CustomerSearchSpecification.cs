using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Specifications
{
    public partial class CustomerSearchSpecification
    {
        partial void BuildExpression()
        {
            var val = 0;
            Add(Extensions.AspNetCore.Mvc.LogicalOperator.AndAlso,
                yy => yy.Id == val);
        }
    }
}
