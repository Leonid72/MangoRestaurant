using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;

namespace Mango.Services.ProductAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponsDto _respons;
        private readonly IProductRepository _productRepository;
        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _respons = new ResponsDto();
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts();
                _respons.Result = productDtos;
            }
            catch (Exception ex)
            {
                _respons.IsSuccess = false;
                _respons.ErrorMessages =
                    new List<string>() { ex.Message.ToString() };
            }
            return Ok(_respons);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(id);
                if (productDto == null)
                {
                    throw new InvalidOperationException("The product was not found.");
                }
                _respons.Result = productDto;
            }
            catch (Exception ex)
            {
                _respons.IsSuccess = false;
                _respons.ErrorMessages =
                 new List<string>() { ex.Message.ToString() };
            }
            return Ok(_respons);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _respons.Result = model;
            }
            catch (Exception ex)
            {
                _respons.IsSuccess = false;
                _respons.ErrorMessages =
                    new List<string>() { ex.Message.ToString() };
            }
            return Ok(_respons);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _respons.Result = model;
            }
            catch (Exception ex)
            {
                _respons.IsSuccess = false;
                _respons.ErrorMessages =
                    new List<string>() { ex.Message.ToString() };
            }
            return Ok(_respons);
        }

        [HttpDelete]
        [Authorize( Roles = "Admin")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
                _respons.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                _respons.IsSuccess = false;
                _respons.ErrorMessages =
                    new List<string>() { ex.Message.ToString() };
            }
            return Ok(_respons);
        }
    }
}
