using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BO;
using Services;

namespace RazorStoreRe.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public EditModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public BO.Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _productService.GetAllProducts() == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["CategoryId"] = new SelectList(_categoryService.GetAllCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            try
            {
                _productService.UpdateProduct(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_productService.GetProductById(id) != null);
        }
    }
}
