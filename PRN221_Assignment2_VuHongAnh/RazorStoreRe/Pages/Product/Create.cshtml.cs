using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BO;
using Services;

namespace RazorStoreRe.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public CreateModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAllCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public BO.Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            int maxId = 0;
            foreach (var product in _productService.GetAllProducts())
            {
                if (product.ProductId > maxId)
                {
                    maxId = product.ProductId;
                }
            }
            Product.ProductId = maxId + 1;

            _productService.AddProduct(Product);

            return RedirectToPage("./Index");
        }
    }
}
