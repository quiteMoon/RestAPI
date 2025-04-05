using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApi.BLL;
using WebApi.BLL.Services.Account;
using WebApi.BLL.Services.Category;
using WebApi.BLL.Services.EmailService;
using WebApi.BLL.Services.Image;
using WebApi.BLL.Services.Product;
using WebApi.BLL.Services.Role;
using WebApi.BLL.Services.User;
using WebApi.BLL.Validators.Account;
using WebApi.DAL;
using WebApi.DAL.Entities.Identity;
using WebApi.DAL.Repositories.Category;
using WebApi.DAL.Repositories.Product;

var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();
//builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("name=PostgresLocal");
});

//var emailSection = builder.Configuration.GetSection("EmailSettings");
//builder.Services.Configure<EmailSettings>(emailSection);

builder.Services
    .AddIdentity<AppUser, AppRole>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

string rootPath = builder.Environment.ContentRootPath;
string wwwroot = Path.Combine(rootPath, "wwwroot");
string imagesPath = Path.Combine(wwwroot, "images");

Settings.ImagePath = imagesPath;

if (!Directory.Exists(wwwroot))
    Directory.CreateDirectory(wwwroot);

if (!Directory.Exists(imagesPath))
    Directory.CreateDirectory(imagesPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/images"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
