using System;
using System.Text;
using Hoshin.CrossCutting.IoC;
using Hoshin.CrossCutting.JWT.Models;
using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.MicrosoftGraph.Configuration;
using Hoshin.WebApi.Configurations;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.CrossCutting.Logger.Implementations;
using Microsoft.AspNetCore.HttpOverrides;
using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.Workflows;
using Hoshin.Infra.AzureStorage.Configuration;
using Microsoft.AspNetCore.Authorization;
using Hoshin.CrossCutting.SignalR;
using Hoshin.WebApi;

namespace Hoshin
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));

            services.AddSession();
            ConfigureContexts(services);

            //services.Configure<MicrosoftGraphAuthSettings>(_config.GetSection(nameof(MicrosoftGraphAuthSettings)));
           // services.Configure<MicrosoftGraphApiSettings>(_config.GetSection(nameof(MicrosoftGraphApiSettings)));
            services.Configure<EmailSettings>(_config.GetSection(nameof(EmailSettings)));
            services.Configure<Workflows>(_config.GetSection("Workflows"));

            ConfigureJWT(services);
            AddSwaggerService(services);
            RegisterServices(services);
            ConfigureAzureStorage(services);

            services.AddAuthorization();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, HasScopeHandler>();


            services.AddMemoryCache();
            services.AddWorkflow(x =>
            {
                x.UseErrorRetryInterval(new TimeSpan(10, 0, 0));
                x.UsePollInterval(new TimeSpan(10, 0, 0));
                x.UseSqlServer(_config.GetConnectionString("DefaultConnection"), false, true);
                //x.UseSqlServer(@"Server=tcp:hoshincloud.database.windows.net,1433;Initial Catalog=HoshinCloudDBTest;Persist Security Info=False;User ID=hoshincloud;Password=H0sh1nCl0ud2019;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", false, true);
                //x.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkflowLocal;Trusted_Connection=True", true, true);
            });

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = _config.GetConnectionString("RedisConnection");
                option.InstanceName = "master";
            });

            services.AddAutoMapper();
            services.AddMvc();
            //services.AddCors();
   
            //Filters
            services.AddScoped<WebApiExceptionFilterAttribute>();
            services.AddScoped<CacheEndpointFilter>();
            services.AddHttpContextAccessor();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });

            services.AddSignalR();
        }        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseCors(builder => {
            //    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");


            app.UseSignalR(routes =>
            {
                routes.MapHub<FindingsHub>("/findings");
            });

            ConfigureSwagger(app);
            ConfigureLogger(loggerFactory);

         

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"assets")),
                RequestPath = "/assets"
            });

            app.UseSession();
            app.UseAuthentication();
            app.UseSpaStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes => {
                routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpa(spa => {
                spa.Options.SourcePath = "../../FrontEnd/ClientApp";
            });


        }
        private void ConfigureAzureStorage(IServiceCollection services)
        {
            var azureStorageBlobOptions = _config.GetSection(nameof(AzureStorageBlobSettings));
            services.Configure<AzureStorageBlobSettings>(options =>
            {
                options.StorageConnectionString = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.StorageConnectionString)];
                options.UrlContainer = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.UrlContainer)];
                options.ContainerFindingName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerFindingName)];
                options.ContainerCorrectiveActionName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerCorrectiveActionName)];
                options.ContainerAuditName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerAuditName)];
                options.ContainerTaskName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerTaskName)];
            });

            services.Configure<Quality.Domain.AzureStorageBlobSettings.AzureStorageBlobSettings>(options =>
            {
                options.StorageConnectionString = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.StorageConnectionString)];
                options.UrlContainer = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.UrlContainer)];
                options.ContainerFindingName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerFindingName)];
                options.ContainerCorrectiveActionName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerCorrectiveActionName)];
                options.ContainerAuditName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerAuditName)];
                options.ContainerTaskName = azureStorageBlobOptions[nameof(AzureStorageBlobSettings.ContainerTaskName)];

            });
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            var jwtAppSettingOptions = _config.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = "module/permission",
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(sharedOptions =>
            {
                
                sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
        }

        private void ConfigureLogger(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
            {
                LogLevel = LogLevel.Information
            }));
        }

        private void AddSwaggerService(IServiceCollection services)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                                        new Info
                                        {
                                            Title = SwaggerConfiguration.DocInfoTitle,
                                            Version = SwaggerConfiguration.DocInfoVersion,
                                            Description = SwaggerConfiguration.DocInfoDescription,
                                            Contact = contact
                                        });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Hoshin.WebApi.xml");
                swagger.IncludeXmlComments(xmlPath);
            });
        }


        private void ConfigureContexts(IServiceCollection services)
        {
            services.AddIdentity<Users, Roles>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 4;
            })
                            .AddEntityFrameworkStores<SQLHoshinCoreContext>()
                            .AddRoles<Roles>()
                            .AddRoleManager<RoleManager<Roles>>()
                            .AddDefaultTokenProviders();
            services.AddDbContext<SQLHoshinCoreContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
                c.RoutePrefix = "api";
            });
        }
        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
