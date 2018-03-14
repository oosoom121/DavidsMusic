using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DavidsMusic.Models;

namespace DavidsMusic
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{		
			Configuration = configuration;
		}

		private Models.ConnectionStrings _connectionStrings;
		public Startup(Microsoft.Extensions.Options.IOptions<Models.ConnectionStrings> connectionStrings)
		{
			_connectionStrings = connectionStrings.Value;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			System.Data.Common.DbConnectionStringBuilder builder =
				new System.Data.Common.DbConnectionStringBuilder();

			services.AddMvc();
			services.AddDistributedMemoryCache();
			services.AddAntiforgery();
			services.AddSession();

					services.Configure<Models.ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
					services.AddOptions();

			services.AddDbContext<Models.DavidTestContext>(opt =>
								opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
	//			, sqlOptions => sqlOptions.MigrationsAssembly(this.GetType().Assembly.FullName))
	//			);
			
			// Options for Password reqirements 
			services.AddIdentity<ApplicationUser, IdentityRole>(
				options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 5;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
			})
				.AddEntityFrameworkStores<Models.DavidTestContext>()
				.AddDefaultTokenProviders();


			services.AddTransient<SendGrid.SendGridClient>((x) =>
			{
				return new SendGrid.SendGridClient(Configuration["sendgrid"]);
			});

			services.AddTransient<Braintree.BraintreeGateway>((x) =>
			{
				return new Braintree.BraintreeGateway(
					Configuration["braintree.environment"],
					Configuration["braintree.merchantid"],
					Configuration["braintree.publickey"],
					Configuration["braintree.privatekey"]
					);
			});

			services.AddTransient<Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient>((x) =>
				{
					Microsoft.WindowsAzure.Storage.CloudStorageAccount account = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(Configuration["StorageConnectionString"]);
					return account.CreateCloudBlobClient();
				});
			
			//		services.AddTransient<SmartyStreets.USStreetApi.Client>((x) =>
			//		{
			//			var client = new SmartyStreets.ClientBuilder(
			//				Configuration["smartystreets.authid"],
			//				Configuration["smartystreets.authtoken"])
			//					.BuildUsStreetApiClient();
			//		
			//			return client;
			//		});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, DavidTestContext context)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink(); // auto updates browser when changes are made
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseDeveloperExceptionPage();

			app.UseSession();
			app.UseStaticFiles();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			DbInitializer.Initialize(context);
		}
	}
}
