using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.BusinessLogicLayer.Concrete;

using E_CommerceWebsite.BusinessLogicLayer.Validations;
using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Concrete;
using E_CommerceWebsite.DataAccesLayer.Data;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddIdentity<AppUser, AppUserRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IValidator<UserDTO>, UserValidator>();
builder.Services.AddScoped<IValidator<ProductDTO>, ProductValidator>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDeliveryAdressesService, DeliveryAdressesService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
        ClockSkew = TimeSpan.Zero

    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = ctx =>
        
        {
            ctx.Request.Cookies.TryGetValue("token", out var token);

            if (!string.IsNullOrEmpty(token))
            {
                ctx.Token = token;
            }

            return Task.CompletedTask;
        }
    };
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseCors(policy => policy
    .WithOrigins("http://localhost:4200")  
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()  
);


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
var imagesPath = Path.Combine(app.Environment.ContentRootPath, "Resources", "Images");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/Resources/Images"
});


app.Run();
