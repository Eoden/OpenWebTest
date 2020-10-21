using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Contact;
using OpenWebApiContract.Communication.LinkContactSkill;
using OpenWebApiContract.Communication.Skill;
using OpenWebData.Database;
using OpenWebData.Repository.v1;
using OpenWebServices.v1.Command;
using OpenWebServices.v1.Query;
using RestServices.BasicAuth.Services;

namespace OpenWebTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOptions();
            
            //Add the Database
            services.AddDbContext<MyDbContext>(options => options.UseInMemoryDatabase("MyDatabase"));


            services.AddControllers();

            //Add the Automapper
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc();

            //Create the Swagger
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "APIOpenWebTest",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "SwaggerOpenWeb API",
                        Description = "Contact API for OpenWeb test",
                        Contact = new OpenApiContact
                        {
                            Name = "Picaud Arthur",
                            Email = "arthur.picaud@supinfo.com"
                        }
                    });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "SwaggerDemo.xml");
                setupAction.IncludeXmlComments(xmlPath);


                // add Basic Authentication
                var basicSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
                };

                 setupAction.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                 setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {basicSecurityScheme, new string[] { }}
                });
            });


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            //Add the differents Interfaces

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ILinkContactSkillRepository, LinkContactSkillRepository>();
            services.AddScoped<IUserService, UserService>();

            //Add the requestHandler binding
            services.AddTransient<IRequestHandler<GetAllContactRequestV1, List<ContactV1>>, GetAllContactsQueryHandler>();
            services.AddTransient<IRequestHandler<GetContactRequestV1, ContactV1>, GetContactByIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateContactRequestV1, ContactV1>, CreateContactCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateContactRequestV1, ContactV1>, UpdateContactCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteContactRequestV1, ContactV1>, DeleteContactCommandHandler>();

            services.AddTransient<IRequestHandler<GetAllSkillRequestV1, List<SkillV1>>, GetAllSkillQueryHandler>();
            services.AddTransient<IRequestHandler<GetSkillRequestV1, SkillV1>, GetSkillByIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateSkillRequestV1, SkillV1>, CreateSkillCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateSkillRequestV1, SkillV1>, UpdateSkillCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteSkillRequestV1, SkillV1>, DeleteSkillCommandHandler>();

            services.AddTransient<IRequestHandler<CreateLinkContactSkillRequestV1, LinkContactSkillV1>, CreateLinkContactSkillCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllLinkContactSkillRequestV1, List<LinkContactSkillV1>>, GetAllLinkContactSkillQueryHandler>();
            services.AddTransient<IRequestHandler<DeleteLinkContactSkillRequestV1, LinkContactSkillV1>, DeleteLinkContactSkillCommandHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDirectoryBrowser();
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();

            //Load the swagger
            app.UseSwagger();

            //Load Swagger UI
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint
                ("/swagger/APIOpenWebTest/swagger.json", "OpenWeb V1");
                setupAction.RoutePrefix = ""; // To available at root
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
