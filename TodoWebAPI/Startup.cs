using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using Todo.Infrastructure.EFRepositories;
using Todo.Infrastructure.Email;
using TodoWebAPI.Data;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.ApplicationServices;
using TodoWebAPI.CronJob;
using TodoWebAPI.ServiceBusRabbitmq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Octokit;
using Octokit.Internal;
using TodoWebAPI.Models;
using TodoWebAPI.Extentions;
using Todo.Infrastructure.Repositories;

namespace TodoWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "Policy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Policy", builder => builder
                    .WithOrigins("http://localhost:5002")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });

            services.AddDbContext<TodoDatabaseContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Development"))
            );
            services.AddScoped<ITodoListRepository, EFTodoListRepository>();
            services.AddScoped<ITodoListLayoutRepository, EFTodoListLayoutRepository>();
            services.AddScoped<ITodoListItemRepository, EFTodoListItemRepository>();
            services.AddScoped<IAccountRepository, EFAccountRepository>();
            services.AddSingleton<IEmailService, DebuggerWindowOutputEmailService>();
            services.AddScoped<IServiceBusEmail, ServiceBusEmail>();
            services.AddScoped<TodoListApplicationService>();
            services.AddScoped<TodoListItemApplicationService>();
            services.AddScoped<TodoListLayoutApplicationService>();
            services.AddScoped<ISubItemRepository, EFSubItemRepository>();
            services.AddScoped<ISubItemLayout, EFSubItemLayout>();
            services.AddScoped<SubItemLayoutApplicationService>();
            services.AddSingleton<DapperQuery>();
            services.AddControllers();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddCronJob<DueDateJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = @"00 12 * * *";
            });

            services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "GitHub";
            })
                .AddCookie()
                .AddOAuth("GitHub", options =>
                {
                    options.ClientId = Configuration["GitHub:ClientId"];
                    options.ClientSecret = Configuration["GitHub:ClientSecret"];
                    options.CallbackPath = new PathString("/external-callback");
                    options.Scope.Add("user:email");

                    options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                    options.UserInformationEndpoint = "https://api.github.com/user";

                    options.SaveTokens = true;

                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapJsonKey("urn:github:login", "login");
                    options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                    options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = async context =>
                        {
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                            context.RunClaimActions(user.RootElement);

                            var github = new GitHubClient(new Octokit.ProductHeaderValue("codefliptodo"), new InMemoryCredentialStore(new Credentials(context.AccessToken)));
                            var primaryEmail = (await github.User.Email.GetAll()).FirstOrDefault(email => email.Primary && email.Verified).Email;

                            // call repo to create or get account
                            var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

                            var account = await mediator.Send(new CreateAccountModel
                            {
                                Email = primaryEmail,
                                FullName = context.Identity.Claims.First(c => c.Type == ClaimTypes.Name)?.Value,
                                PictureUrl = context.Identity.Claims.First(c => c.Type == "urn:github:avatar").Value
                            });

                            context.Identity.AddClaim(new Claim("urn:codefliptodo:accountid", account.Id.ToString()));

                            context.Identity.AddClaim(new Claim(
                            ClaimTypes.Email, primaryEmail,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer));
                        }
                    };
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseFileServer();

            app.UseAuthentication();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
