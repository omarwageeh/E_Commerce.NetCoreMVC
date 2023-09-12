
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Model
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public bool isActive { get; set; }
    }
}
