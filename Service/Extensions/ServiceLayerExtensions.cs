using Core.Entities;
using DataAccess;
using DataAccess.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.FluentValidations;
using Service.Helpers.Images;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Extensions
{
    public static class ServiceLayerextensions
	{
		public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
			services.AddScoped<IDashbordService, DashboardService>();
            services.AddAutoMapper(assembly);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();	
			services.AddScoped<IImageHelper, ImageHelper>();
			services.AddControllersWithViews().AddFluentValidation(opt =>
			{
				opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
				opt.DisableDataAnnotationsValidation = true;
				opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("en-US");
			});
			return services;
		}
	}
}
