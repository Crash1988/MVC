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
using ContosoUniversity.Models;
using ContosoUniversity.Services;

namespace ContosoUniversity
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
            this.Seed(app);
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
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
        private void Seed(IApplicationBuilder app)
        {
            using (var db = app.ApplicationServices.GetService<ApplicationDbContext>())
            {
                if (db.Students.Count() > 0)
                    return;

                var students = new List<Student>
                {
                    new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("01-09-2005")},
                    new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("01-09-2001")},
                    new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse( "01-09-2001")},
                    new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("01-09-2002")},
                    new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("01-09-2002")},
                    new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse ("01-09-2001")},
                    new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse( "01-09-2003")},
                    new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse ("01-09-2005")}
                };
                students.ForEach(s => db.Students.Add(s));
                db.SaveChanges();
                var courses = new List<Course>
                {
                    new Course { CourseID = 1050, Title = "Chemistry", Credits = 3, },
                    new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3, },
                    new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3, },
                    new Course { CourseID = 1045, Title = "Calculus", Credits = 4, },
                    new Course { CourseID = 3141, Title = "Trigonometry", Credits = 4, },
                    new Course { CourseID = 2021, Title = "Composition", Credits = 3, },
                    new Course { CourseID = 2042, Title = "Literature", Credits = 4, }
                };
                courses.ForEach(s => db.Courses.Add(s));
                db.SaveChanges();
                var enrollments = new List<Enrollment>
                {
                    new Enrollment { StudentID = 1, CourseID = 1050, Grade = Grade.A },
                    new Enrollment { StudentID = 1, CourseID = 4022, Grade = Grade.C },
                    new Enrollment { StudentID = 1, CourseID = 4041, Grade = Grade.B },
                    new Enrollment { StudentID = 2, CourseID = 1045, Grade = Grade.B },
                    new Enrollment { StudentID = 2, CourseID = 3141, Grade = Grade.F },
                    new Enrollment { StudentID = 2, CourseID = 2021, Grade = Grade.F },
                    new Enrollment { StudentID = 3, CourseID = 1050 },
                    new Enrollment { StudentID = 4, CourseID = 1050, },
                    new Enrollment { StudentID = 4, CourseID = 4022, Grade = Grade.F },
                    new Enrollment { StudentID = 5, CourseID = 4041, Grade = Grade.C },
                    new Enrollment { StudentID = 6, CourseID = 1045 },
                    new Enrollment { StudentID = 7, CourseID = 3141, Grade = Grade.A },
                };
                enrollments.ForEach(s => db.Enrollments.Add(s));
                db.SaveChanges();


                        // Seed code

                        db.SaveChanges();
            }
        }
    }
}
