using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto,Product>(productDto);
            if (product.ProductId > 0)
            {
                _dbContext.Update(product);
            }
            else
            {
                _dbContext.Add(product);
            }
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<Product,ProductDto>(product);          
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
                if(product == null)
                    return false;
                _dbContext.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
