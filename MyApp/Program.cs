using DataAccess;
using DataAccess.Extensions;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Filters.ArticleVisitors;
using NToastNotify;
using Service.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();

builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews(opt =>
{
	opt.Filters.Add<ArticleVisitorFilter>();
})
	.AddNToastNotifyToastr(new ToastrOptions()
	{
		PositionClass = ToastPositions.TopRight,
		TimeOut = 3000,
	})
	.AddRazorRuntimeCompilation();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireUppercase = false;
	opt.Password.RequireLowercase = false;

})
	.AddRoleManager<RoleManager<AppRole>>()
	.AddEntityFrameworkStores<RepositoryContext>()
	.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
	config.LoginPath = new PathString("/Admin/Auth/Login");
	config.LogoutPath = new PathString("/Admin/Auth/Logout");
	config.Cookie = new CookieBuilder
	{
		Name = "MyApp",
		HttpOnly = true,
		SameSite = SameSiteMode.Strict,
		SecurePolicy = CookieSecurePolicy.SameAsRequest
	};
	config.SlidingExpiration = true;
	config.ExpireTimeSpan = TimeSpan.FromDays(7);
	config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");

});
	

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapAreaControllerRoute(
		name: "Admin",
		areaName: "Admin",
		pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
		);
	endpoints.MapDefaultControllerRoute();
}
);

app.Run();
