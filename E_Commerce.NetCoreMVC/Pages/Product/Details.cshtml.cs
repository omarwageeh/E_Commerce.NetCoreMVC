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
using E_Commerce.Service;

namespace E_Commerce.NetCoreMVC.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public DetailsModel(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProducts(m => m.Id == id);
            if (product!.FirstOrDefault() == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product.FirstOrDefault()!;
            }
            return Page();
        }
    }
}
