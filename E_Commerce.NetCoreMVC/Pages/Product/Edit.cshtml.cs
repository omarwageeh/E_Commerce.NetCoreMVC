using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data.Context;
using E_Commerce.Model;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.Repository.Interface;
using E_Commerce.Service;

namespace E_Commerce.NetCoreMVC.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        public EditModel(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            Categories = _categoryService.GetCategories().Result!.Select(c =>
                                  new SelectListItem
                                  {
                                      Value = c.Id.ToString(),
                                      Text = c.Name
                                  }).ToList();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> Categories { get; set; }
        [BindProperty]
        public string CategoryId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  (await _productService.GetProducts(p=>p.Id == id))!.FirstOrDefault();
            CategoryId = product.CategoryId.ToString();
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product.Id = Guid.Parse(id!);
            Product.CategoryId = Guid.Parse(CategoryId);
            await _productService.UpdateProduct(Product);

            return RedirectToPage("./Index");
        }
    }
}
