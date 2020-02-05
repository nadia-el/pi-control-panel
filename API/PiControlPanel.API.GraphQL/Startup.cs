namespace PiControlPanel.API.GraphQL
{
    using System;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Boxed.AspNetCore;
    using global::GraphQL.Server;
    using global::GraphQL.Server.Ui.Playground;
    using LightInject;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using PiControlPanel.API.GraphQL.Extensions;
    using PiControlPanel.API.GraphQL.Schemas;
    using PiControlPanel.Application.Services;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Contracts.Constants;
    
    /// <summary>
    /// Application startup class.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">IConfiguration.</param>
        /// <param name="webHostEnvironment">IHostingEnvironment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(); // enables Access-Control-Allow-Origin (angular calling graphql methods)

            services.AddGraphQL(options => { options.ExposeExceptions = true; })
                .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User });

            services.AddCustomGraphQL(this.webHostEnvironment);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };

                    if (webHostEnvironment.IsDevelopment())
                    {
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    }
                });

            services.AddGraphQLAuth(settings =>
            {
                settings.AddPolicy(AuthorizationPolicyName.Authenticated,
                    p => p.RequireClaim(CustomClaimTypes.IsAnonymous, new string[] { bool.FalseString }));
                settings.AddPolicy(AuthorizationPolicyName.Individual,
                    p => p.RequireClaim(ClaimTypes.Role, new string[] { Roles.Individual }));
            });

            services.AddHealthChecks();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // Allowing Synchronous calls to be made in the pipeline
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        /// <summary>
        ///     This method is used to add services directly to LightInject
        /// </summary>
        /// <param name="container">LightInject service container</param>
        public void ConfigureContainer(IServiceContainer container)
        {
            container.AddGraphQLServicesDependency();

            container.RegisterScoped<IControlPanelService, ControlPanelService>();

            //Registers all services required for the Application layer
            container.RegisterFrom<AppCompositionRoot>();

            container.RegisterSingleton<IConfiguration>(factory => configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder.</param>
        /// <param name="env">IWebHostEnvironment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            app.UseIf(
                    !webHostEnvironment.IsProduction(),
                    x => x
                        .UseDeveloperExceptionPage()
                        .UseGraphQLPlayground(new GraphQLPlaygroundOptions() { Path = "/" }));

            // enables Access-Control-Allow-Origin (angular calling webapi methods)
            app.UseCors(builder => builder
               .WithOrigins("http://localhost:54532")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            // Add GraphQL web sockets middleware to the request pipeline (support for subscriptions)
            app.UseWebSockets();
            app.UseGraphQLWebSockets<ControlPanelSchema>();

            app.UseForwardedHeaders()
                .UseAuthentication()
                .UseHealthChecks("/healthcheck")
                .UseGraphQL<ControlPanelSchema>();
        }
    }
}
