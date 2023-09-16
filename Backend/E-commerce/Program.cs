using E_commerce.Database;
using E_commerce.models;
using E_commerce.Repo.Product;
using E_commerce.Repo.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database
var connectionString = builder.Configuration.GetConnectionString("DataBase_Connections");
builder.Services.AddDbContext<E_commerceDataBase>(options =>
{
    options.UseNpgsql(connectionString);
});
#endregion



#region Manager
builder.Services.AddScoped<HttpContextAccessor>();
#endregion

///Microsoft.AspNetCore.Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        // Allow requests from any origin, method, and header
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



#region identity
builder.Services.AddIdentity<Users, IdentityRole>()
    .AddEntityFrameworkStores<E_commerceDataBase>();
#endregion



#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "cool";
    options.DefaultChallengeScheme = "cool";
}).AddJwtBearer("cool", options =>
{
    string keyString = builder.Configuration.GetValue<string>("secretkey")!;
    var keyInByte = Encoding.ASCII.GetBytes(keyString);
    // Convert keyString to security key
    var key = new SymmetricSecurityKey(keyInByte);
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        IssuerSigningKey = key,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion




var app = builder.Build();

//var url = "https://192.168.1.3:8000"; // Set the desired URL here
//app.Urls.Add(url);


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRouting(); // This middleware is required for routing.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // Enable authentication before authorization.

app.UseAuthorization();

app.UseCors(); // UseCors should be placed after UseAuthorization.

app.MapControllers();

app.Run();

