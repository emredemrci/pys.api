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




using Microsoft.EntityFrameworkCore;
using pys.api.Data;
using pys.api.Services;

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

// 🔹 CORS Aktif Et
app.UseCors("AllowAll");

// 🔹 Routing (Yönlendirme)
app.UseRouting();

// 🔹 Authentication & Authorization (Eğer gerekiyorsa)
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

//var builder = WebApplication.CreateBuilder(args);

//// SQL Server bağlantısını ekleyelim
//builder.Services.AddDbContext<PYSDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// MVC desteği ekleyelim
//builder.Services.AddControllersWithViews();

//// **URL belirleme**
//var url = builder.Configuration["ApplicationUrl"] ?? "http://localhost:4000";
//builder.WebHost.UseUrls(url); // ✅ Doğru yöntem

//// Web uygulaması oluştur
//var app = builder.Build();

//// Eğer geliştirme ortamında değilsek, hata sayfası ve HSTS kullan
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//// HTTPS yönlendirme, statik dosyalar ve routing ekleyelim
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();

//// Varsayılan rota tanımlayalım
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//// Uygulamayı başlatalım
//app.Run();

