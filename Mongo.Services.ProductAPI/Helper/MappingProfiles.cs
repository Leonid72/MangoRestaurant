using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
        }
    }
}
