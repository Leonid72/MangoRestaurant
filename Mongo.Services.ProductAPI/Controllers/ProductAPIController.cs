using Microsoft.AspNetCore.Mvc;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;

namespace Mango.Services.ProductAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
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
        public async Task<object> Get()
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
            return _respons;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(id);
                _respons.Result = productDto;
            }
            catch (Exception ex)
            {
                _respons.IsSuccess = false;
                _respons.ErrorMessages =
                    new List<string>() { ex.Message.ToString() };
            }
            return _respons;
        }

        [HttpPost]
        public async Task<object> Post([FromBody]ProductDto productDto)
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
            return _respons;
        }

        [HttpPut]
        public async Task<object> Put([FromBody]ProductDto productDto)
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
            return _respons;
        }

        [HttpDelete]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
                _respons.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _respons.IsSuccess = false;
                _respons.ErrorMessages =
                    new List<string>() { ex.Message.ToString() };
            }
            return _respons;
        }
    }
}
