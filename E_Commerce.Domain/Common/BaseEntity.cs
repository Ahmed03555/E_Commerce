using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Common
{
    public class BaseEntity<Tkey>
    {
        public Tkey Id { get; set; } = default!;
    }
}
