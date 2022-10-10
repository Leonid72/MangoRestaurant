using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _productService.GetAllProductsAsync<ResponseDto>(accessToken);
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
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.CreataProductAsync<ResponseDto>(model, accessToken);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
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
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.UpdateProductAsync<ResponseDto>(model, accessToken);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (res.Result != null && res.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(res.Result.ToString()!)!;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            //if (ModelState.IsValid)
            if(model.ProductId != null)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
                if (res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
    }
}
