using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Domains.Entities._Examples
{
    public class CustomerEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public OneEntity One { get; set; }

        public ICollection<ManyEntity> Many { get; set; }
    }
}
