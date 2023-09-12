using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Commerce.Data.Context;
using E_Commerce.Model;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.NetCoreMVC.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {

        private readonly E_Commerce.Data.Context.DataContext _context;

        public CreateModel(E_Commerce.Data.Context.DataContext context)
        {
            _context = context;
            Categories = _context.Categories.Select(c =>
                                  new SelectListItem
                                  {
                                      Value = c.Id.ToString(),
                                      Text = c.Name
                                  }).ToList();
        }

        public IActionResult OnGet()
        {
            
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        
        public List<SelectListItem> Categories { get; set; }
        [BindProperty]
        public string SelectedCategoryId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Products == null || Product == null)
            {
                return Page();
            }
            var cat =  _context.Categories.Find(Guid.Parse(SelectedCategoryId));
            if(cat == null)
            {
                return NotFound();
            }
            Product.Category = cat!;
            _context.Products.Add(Product);
             _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
