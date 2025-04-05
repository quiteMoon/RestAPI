using AutoMapper;
using WebApi.BLL.Dtos.Category;
using WebApi.DAL.Entities;

namespace WebApi.BLL.MapperProfiles
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            // CreateCategotyDto -> CategoryEntity
            CreateMap<CreateCategoryDto, CategoryEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            // UpdateCategoryDto -> CategotyEntity
            CreateMap<UpdateCategoryDto, CategoryEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            // CategotyEntity -> CategoryDto
            CreateMap<CategoryEntity, CategoryDto>();
        }
    }
}
