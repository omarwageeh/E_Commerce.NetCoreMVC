using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Model
{
    public class Admin : User
    {
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
    }
}
