using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data.Context;
using E_Commerce.Model;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.Repository.Interface;

namespace E_Commerce.NetCoreMVC.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _context;

        public IndexModel(IUnitOfWork context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;
        public async Task OnGetAsync()
        {
                Product = (await _context.ProductRepository.GetAllWithInclude(p=>!p.IsDeleted, "Category")).ToList();
        }
    }
}
