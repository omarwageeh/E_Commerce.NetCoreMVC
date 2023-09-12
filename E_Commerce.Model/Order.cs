using E_Commerce.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Model
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public Customer Customer { get; set; }
        public Status Status { get; set; }
    }
}
