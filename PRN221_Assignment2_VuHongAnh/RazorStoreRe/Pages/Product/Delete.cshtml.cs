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
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
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
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _productService.GetAllProducts() == null)
            {
                return NotFound();
            }
            var product = _productService.GetProductById(id);

            if (product != null)
            {
                Product = product;
                _productService.DeleteProduct(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
