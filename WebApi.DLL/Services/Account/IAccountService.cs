﻿using WebApi.BLL.Dtos.Account;

namespace WebApi.BLL.Services.Account
{
    public interface IAccountService
    {
        Task<ServiceResponse> LoginAsync(LoginDto dto);
        Task<ServiceResponse> RegisterAsync(RegisterDto dto);
        Task<ServiceResponse> ConfirmEmailAsync(string userId, string token);
    }
}
