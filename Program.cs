using System.Text;
using HelloApi.Repositories;
using HelloApi.Services;
using MessageApi.Data;
using MessageApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HelloApi.Repositories.Interfaces;
using HelloApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependencias
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IRoleRepository, RoleRepository>();
//builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();



builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// CORS
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");
var keyBytes = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "YourSuperSecretKey123!");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
//    };
//});

//builder.Services.AddAuthorization();

var app = builder.Build();


app.UseCors("CorsPolicy");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
// Apply DB migration and seed data
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}

app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();