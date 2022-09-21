using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService service)
        {
            _productService = service;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> products = new();
            var res = await _productService.GetAllProductsAsync<ResponsDto>();
            if (res != null && res.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(res.Result.ToString()!)!;
            }
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.CreataProductAsync<ResponsDto>(model);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {         
            var res = await _productService.GetProductByIdAsync<ResponsDto>(productId);
            if (res.Result != null && res.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(res.Result.ToString()!)!;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.UpdateProductAsync<ResponsDto>(model);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            var res = await _productService.GetProductByIdAsync<ResponsDto>(productId);
            if (res.Result != null && res.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(res.Result.ToString()!)!;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            //if (ModelState.IsValid)
            if(model.ProductId != null)
            {
                var res = await _productService.DeleteProductAsync<ResponsDto>(model.ProductId);
                if (res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
    }
}
