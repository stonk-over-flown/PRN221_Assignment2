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
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public IndexModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IList<BO.Product> Product { get;set; } = default!;
        public List<string> CategoryName { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            if (_productService.GetAllProducts() != null)
            {
                Product = _productService.GetAllProducts();
                foreach (BO.Product product in Product)
                {
                    CategoryName.Add(_categoryService.GetCategoryById(product.CategoryId).CategoryName);
                }
            }
        }
    }
}
