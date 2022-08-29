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

        public async Task<IActionResult>  ProductIndex()
        {
            List<ProductDto> products = new();
            var res = await _productService.GetAllProductsAsync<ResponsDto>();
            if(res != null && res.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(res.Result.ToString()!)!;
            }
            return View(products);
        }
    }
}
