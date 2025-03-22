//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using pys.api.Data;
//using pys.api.Services;
//using System.Text;
//using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);

//// 🔹 SQL Server Bağlantısı
//builder.Services.AddDbContext<PYSDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// 🔹 Servis Bağımlılıklarını Enjekte Et
//builder.Services.AddScoped<IPersonnelSalaryService, PersonnelSalaryService>();
//builder.Services.AddScoped<IPersonnelService, PersonnelService>();

//// 🔹 Controller Desteği Ekleyelim
//builder.Services.AddControllersWithViews();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "PYS API",
//        Version = "v1",
//        Description = "PYS Web API Dökümantasyonu",
//        Contact = new OpenApiContact
//        {
//            Name = "Emre",
//            Email = "emre@example.com",
//            Url = new Uri("https://github.com/emre"),
//        }
//    });
//});

//// 🔹 CORS Desteği (Eğer frontend ile iletişim olacaksa)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

//// 🔹 JWT Yapılandırması
//var jwtSettings = builder.Configuration.GetSection("Jwt");
//var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!); // "Key" yerine "Secret" kullanıldı

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidIssuer = jwtSettings["Issuer"],
//            ValidAudience = jwtSettings["Audience"],
//            ValidateLifetime = true,
//            ClockSkew = TimeSpan.Zero  // Token süresi dolduğunda hemen geçersiz olsun
//        };
//    });

//builder.Services.AddAuthorization();
//builder.Services.AddScoped<IJwtService, JwtService>();



//// ✅ Web Uygulamasını Oluştur
//var app = builder.Build();

//// 🔹 Hata Yönetimi & Güvenlik Ayarları
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//// 🔹 HTTPS Yönlendirme & Statik Dosyalar
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//// 🔹 Routing (Yönlendirme)
//app.UseRouting();

//// 🔹 CORS Aktif Et (Yönlendirmeden önce)
//app.UseCors("AllowAll");

//// 🔹 Authentication & Authorization (Kimlik Doğrulama & Yetkilendirme)
//app.UseAuthentication();
//app.UseAuthorization();

//// 🔹 Varsayılan Rota Tanımlaması
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//// ✅ Uygulamayı Başlat
//app.Run();
















































using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pys.api.Data;
using pys.api.Services;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 SQL Server Bağlantısı
builder.Services.AddDbContext<PYSDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Servis Bağımlılıklarını Enjekte Et
builder.Services.AddScoped<IPersonnelSalaryService, PersonnelSalaryService>();
builder.Services.AddScoped<IPersonnelService, PersonnelService>();

// 🔹 Controller Desteği Ekleyelim
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PYS API",
        Version = "v1",
        Description = "PYS Web API Dokümantasyonu",
        Contact = new OpenApiContact
        {
            Name = "Emre",
            Email = "emre@example.com",
            Url = new Uri("https://github.com/emre"),
        }
    });

    // ✅ Swagger İçin JWT Desteği Ekleyelim
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Lütfen 'Bearer <token>' formatında JWT tokenınızı girin."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 🔹 CORS Desteği (Eğer frontend ile iletişim olacaksa)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🔹 JWT Yapılandırması
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!); // "Key" yerine "Secret" kullanıldı

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero  // Token süresi dolduğunda hemen geçersiz olsun
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<IJwtService, JwtService>();

// ✅ Web Uygulamasını Oluştur
var app = builder.Build();

// 🔹 Hata Yönetimi & Güvenlik Ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 🔹 HTTPS Yönlendirme & Statik Dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// 🔹 Routing (Yönlendirme)
app.UseRouting();

// 🔹 CORS Aktif Et (Yönlendirmeden önce)
app.UseCors("AllowAll");

// 🔹 Authentication & Authorization (Kimlik Doğrulama & Yetkilendirme)
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Swagger Middleware’i Ekleyelim
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PYS API v1");
        options.RoutePrefix = string.Empty; // Swagger ana sayfa olarak açılsın
    });
}

// 🔹 Varsayılan Rota Tanımlaması
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Uygulamayı Başlat
app.Run();
