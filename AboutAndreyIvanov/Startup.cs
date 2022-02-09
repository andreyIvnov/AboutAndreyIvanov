using AboutAndreyIvanov.Domain;
using AboutAndreyIvanov.Domain.Repositories.Abstract;
using AboutAndreyIvanov.Domain.Repositories.EntityFramework;
using AboutAndreyIvanov.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AboutAndreyIvanov
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
       
        public void ConfigureServices(IServiceCollection services)
        {
            //use config from appsetings.json
            Configuration.Bind("Project", new Config());

            #region Functionality adding by services
            
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();     //Conneting of Interface to realization (EntityFramework)
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            #endregion

            #region Connection to DB with context

            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            #endregion

            #region Identity system configurations

            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            #endregion

            #region Authentication cookie settings 

            services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie.Name = "myCompanyAuth";
                opts.Cookie.HttpOnly = true;                        // no access on clien side
                opts.LoginPath = "/account/login";
                opts.AccessDeniedPath = "/account/accessdenied";
                opts.SlidingExpiration = true;
            });

            #endregion

            //adding a Controllers and MVC Views support
            services.AddControllersWithViews(). // workinkg with MVC wies
                SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddCookieTempDataProvider(); //Colibration with asp core 3.0
        }   

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //MIDDLEWARE registration procedure is important

            #region Exceptions catcher in process of dev 

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            #endregion

            #region Connect static files to the app (css, jpg, js ....)
            app.UseStaticFiles();
            #endregion

            #region Routing system

            app.UseRouting();

            #endregion

            #region Authentication and Authorization connect

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            #endregion

            #region Registration of URL's we need (EndPoints)

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            #endregion

        }
    }
}
