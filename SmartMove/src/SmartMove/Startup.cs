using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartMove.Models;
using SmartMove.Services;

namespace SmartMove
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
          //  this.Seed(app);       //use this to seed the database
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name:"first",
                    template: "{controller=Home}/{action=Index}/{id}/{teamid}"
                    );
            });
        }

        private void Seed(IApplicationBuilder app)
        {
            using (var db = app.ApplicationServices.GetService<ApplicationDbContext>())
            {
                if (db.Team.Count() > 0)
                    return;

                var Teams = new List<Team>
                {
                    new Team{Name="Baltimore Orioles",Logo="logo_bal_79x76.jpg"},
                    new Team{Name="Arizona Diamondbacks",Logo="logo_ari_79x76.jpg"},
                    new Team{Name="Boston Red Sox",Logo="logo_bos_79x76.jpg"},
                    new Team{Name="Atlanta Braves",Logo="logo_atl_79x76.jpg"},
                    new Team{Name="Chicago White Sox",Logo="logo_cws_79x76.jpg"},
                    new Team{Name="Chicago Cubs",Logo="logo_chc_79x76.jpg"},
                    new Team{Name="Cleveland Indians",Logo="logo_cle_79x76.jpg"},
                    new Team{Name="Cincinnati Reds",Logo="logo_cin_79x76.jpg"},
                    new Team{Name="Detroit Tigers",Logo="logo_det_79x76.jpg"},
                    new Team{Name="Colorado Rockies",Logo="logo_col_79x76.jpg"},
                    new Team{Name="Houston Astros",Logo="logo_hou_79x76.jpg"},
                    new Team{Name="Los Angeles Dodgers",Logo="logo_la_79x76.jpg"},
                    new Team{Name="Kansas City Royals",Logo="logo_kc_79x76.jpg"},
                    new Team{Name="Miami Marlins",Logo="logo_mia_79x76.jpg"},
                    new Team{Name="Los Angeles Angels",Logo="logo_ana_79x76.jpg"},
                    new Team{Name="Milwaukee Brewers",Logo="logo_mil_79x76.jpg"},
                    new Team{Name="Minnesota Twins",Logo="logo_min_79x76.jpg"},
                    new Team{Name="New York Mets",Logo="logo_nym_79x76.jpg"},
                    new Team{Name="New York Yankees",Logo="logo_nyy_79x76.jpg"},
                    new Team{Name="Philadelphia Phillies",Logo="logo_phi_79x76.jpg"},
                    new Team{Name="Oakland Athletics",Logo="logo_oak_79x76.jpg"},
                    new Team{Name="Pittsburgh Pirates",Logo="logo_pit_79x76.jpg"},
                    new Team{Name="Seattle Mariners",Logo="logo_sea_79x76.jpg"},
                    new Team{Name="San Diego Padres",Logo="logo_sd_79x76.jpg"},
                    new Team{Name="Tampa Bay Rays",Logo="logo_tb_79x76.jpg"},
                    new Team{Name="San Francisco Giants",Logo="logo_sf_79x76.jpg"},
                    new Team{Name="Texas Rangers",Logo="logo_tex_79x76.jpg"},
                    new Team{Name="St. Louis Cardinals",Logo="logo_stl_79x76.jpg"},
                    new Team{Name="Toronto Blue Jays",Logo="logo_tor_79x76.jpg"},
                    new Team{Name="Washington Nationals",Logo="logo_was_79x76.jpg"},
                };
                Teams.ForEach(s => db.Team.Add(s));
                db.SaveChanges();
            }
        }
        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
