
using CrwnClothing.BLL.Helpers;
using CrwnClothing.BLL.Services;
using CrwnClothing.BLL.Services.ColorService;
using CrwnClothing.BLL.Services.External;
using CrwnClothing.BLL.Services.NotificationService;
using CrwnClothing.BLL.Services.OrderService;
using CrwnClothing.BLL.Services.Payments;
using CrwnClothing.BLL.Services.ShoppingCart;
using CrwnClothing.BLL.Services.ShppingCart;
using CrwnClothing.BLL.Services.Sizes;
using CrwnClothing.BLL.Settings;
using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Repositories.CategoryRepository;
using CrwnClothing.DAL.Repositories.ColorRepository;
using CrwnClothing.DAL.Repositories.OrderRepository;
using CrwnClothing.DAL.Repositories.ProductRepository;
using CrwnClothing.DAL.Repositories.ShoppingCartProductRepository;
using CrwnClothing.DAL.Repositories.ShoppingCartRepository;
using CrwnClothing.DAL.Repositories.SizeRepository;
using CrwnClothing.DAL.Repositories.UserRepository;
using CrwnClothing.Presentation.Attributes;
using CrwnClothing.Presentation.Helpers;
using CrwnClothing.Presentation.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

#region[CORS]
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://192.168.1.2:3000")
        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});
#endregion

#region[SQLPOOL]
builder.Services.AddDbContextPool<CrwnClothingContext>(options =>
        options.UseSqlServer
        (
            "Server=NEMUS\\SQLEXPRESS;Database=crwn-clothing;Trusted_Connection=True;"
        ));
#endregion

#region[SERVICES]
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartProductRepository, ShoppingCartProductRepository>();
builder.Services.AddScoped<IShoppingCartProductService, ShoppingCartProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IFacebookAuthService, FacebookAuthService>();
builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
builder.Services.AddScoped<IPaymentService, StripePaymentService>();
builder.Services.AddScoped<HeaderParser>();
builder.Services.AddScoped<JwtAuthenticationManager>();
builder.Services.AddScoped<StripeAuthorizationManager>();
builder.Services.AddScoped<ICcPaymentService, CcPaymentService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IUserNotificationService, UserNotificationService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(options => new Dictionary<string, UserConnection>());
builder.Services.AddSingleton(sp => {
    var logger = sp.GetRequiredService<ILogger<ProductInventoryHub>>();
    return new ProductInventoryHub(logger);
});
builder.Services.AddHttpClient<IHttpClientBuilder>();
builder.Services.AddSignalR();
#endregion

#region[AUTH]
string key = builder.Configuration["AppSettings:JWTEncryptionKey"];

builder.Services.AddAuthentication(builder =>
{
    builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(builder =>
{
    builder.RequireHttpsMetadata = false;
    builder.SaveToken = true;
    builder.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

#region[EMAIL_CONFIGURATION]
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
#endregion

#region[REDIS]
var redisConfiguration = builder.Configuration.GetSection("Redis").Get<RedisConfiguration>();
builder.Services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
builder.Services.AddDistributedMemoryCache();
#endregion

var app = builder.Build();

var environment = builder.Environment;

PathRegistry.GetInstance(contentRootPath: environment.ContentRootPath,
    webRootPath: Path.Combine(environment.ContentRootPath, "wwwroot"),
    templateRootPath: Path.Combine(environment.ContentRootPath, "Templates"));


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-dev");
}
else
{
    app.UseExceptionHandler("/Error");
}


app.MapControllers();

app.UseRouting();

app.UseCors();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat");
    endpoints.MapHub<ProductInventoryHub>("/product-inventory");
});


app.Run();
