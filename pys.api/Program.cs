//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using pys.api.Data;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PYSDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.Run();












//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using pys.api.Data;
//using pys.api.Services;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// 🔹 SQL Server Bağlantısı
//builder.Services.AddDbContext<PYSDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// 🔹 Servis Bağımlılıklarını Enjekte Et
//builder.Services.AddScoped<IPersonnelSalaryService, PersonnelSalaryService>();
//builder.Services.AddScoped<IPersonnelService, PersonnelService>();

//// 🔹 Controller Desteği Ekleyelim
//builder.Services.AddControllersWithViews();

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
//var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

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
//            ClockSkew = TimeSpan.Zero  // Token süresi tam dolduğunda geçersiz olsun
//        };
//    });

//builder.Services.AddAuthorization();

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

//// 🔹 CORS Aktif Et
//app.UseCors("AllowAll");

//// 🔹 Authentication & Authorization (Kimlik Doğrulama & Yetkilendirme)
//app.UseAuthentication();
//app.UseAuthorization();

//// 🔹 Routing (Yönlendirme)
//app.UseRouting();

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

var builder = WebApplication.CreateBuilder(args);

// 🔹 SQL Server Bağlantısı
builder.Services.AddDbContext<PYSDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Servis Bağımlılıklarını Enjekte Et
builder.Services.AddScoped<IPersonnelSalaryService, PersonnelSalaryService>();
builder.Services.AddScoped<IPersonnelService, PersonnelService>();

// 🔹 Controller Desteği Ekleyelim
builder.Services.AddControllersWithViews();

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

// 🔹 Varsayılan Rota Tanımlaması
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Uygulamayı Başlat
app.Run();
















































//using Microsoft.EntityFrameworkCore;
//using pys.api.Data;
//using pys.api.Services;

//var builder = WebApplication.CreateBuilder(args);

//// 🔹 SQL Server Bağlantısı
//builder.Services.AddDbContext<PYSDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// 🔹 Servis Bağımlılıklarını Enjekte Et
//builder.Services.AddScoped<IPersonnelSalaryService, PersonnelSalaryService>();
//builder.Services.AddScoped<IPersonnelService, PersonnelService>();

//// 🔹 Controller Desteği Ekleyelim
//builder.Services.AddControllersWithViews();

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

//// 🔹 CORS Aktif Et
//app.UseCors("AllowAll");

//// 🔹 Routing (Yönlendirme)
//app.UseRouting();

//// 🔹 Authentication & Authorization (Eğer gerekiyorsa)
//app.UseAuthentication();
//app.UseAuthorization();

//// 🔹 Varsayılan Rota Tanımlaması
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//// ✅ Uygulamayı Başlat
//app.Run();


