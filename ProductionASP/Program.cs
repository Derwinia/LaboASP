using Microsoft.EntityFrameworkCore;
using ProductionASP.Services;
using ProductionDAL;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProductContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("CNX")));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<SmtpClient>();
builder.Services.AddSingleton(builder.Configuration.GetSection("SMTP").Get<MailConfig>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
