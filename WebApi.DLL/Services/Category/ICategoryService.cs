﻿using WebApi.BLL.Dtos.Category;

namespace WebApi.BLL.Services.Category
{
    public interface ICategoryService
    {
        Task<ServiceResponse> CreateAsync(CreateCategoryDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateCategoryDto dto);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByNameAsync(string name);
    }
}
