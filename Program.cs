using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Models;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);


// Ba�lant� dizisi
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Latin harflerini desteklemesi i�in
builder.Services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA }));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

// HttpContextAccessor'� ekleyin
builder.Services.AddHttpContextAccessor();

// Di�er hizmetleri ekleyin
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();



app.Use(async (context, next) =>
{
	var endpoint = context.GetEndpoint();

	
	// HomeController veya di�er controller'lar i�in
	if (endpoint != null)
	{
		var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

	   if (controllerActionDescriptor != null && controllerActionDescriptor.ControllerName != "User")
		{
			await next();
			return;
		}

		else
		{
			if (!context.Session.Keys.Contains("UserId"))
			{
				context.Response.Redirect("/Home/Login"); // E�er session yoksa, login sayfas�na y�nlendir.
				return;
			}
		}
	}

	// UserController i�in
	await next();
});


app.MapControllerRoute(
	name: "Default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
