using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BO;
using Services;

namespace RazorStoreRe.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public DetailsModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public BO.Product Product { get; set; } = default!; 
        public string CategoryName { get; set; } = "";

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
            else 
            {
                CategoryName = _categoryService.GetCategoryById(product.CategoryId).CategoryName;
                Product = product;
            }
            return Page();
        }
    }
}
