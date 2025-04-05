using AutoMapper;
using WebApi.BLL.Dtos.Product;
using WebApi.DAL.Entities;

namespace WebApi.BLL.MapperProfiles
{
    class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            // CreateProductDto -> ProducEntity
            CreateMap<CreateProductDto, ProductEntity>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

            // UpdateProductDto -> ProducEntity
            CreateMap<UpdateProductDto, ProductEntity>();

            // ProducEntity -> ProductDto
            CreateMap<ProductEntity, ProductDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImagePath)));
        }
    }
}
