using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Model
{
    public class Customer : User
    {
        [MaxLength(12)]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
