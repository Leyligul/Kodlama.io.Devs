using Application.Features.OperationClaims.Rules;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.ProgrammingLanguageTechnologies.Rules;
using Application.Features.UserOperationClaims.Rules;
using Application.Features.Users.Rules;
using Application.Features.Websites.Rules;
using Application.Services.AuthServıce;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using Core.Security.JWT;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<ProgrammingLanguageTechnologysRules>();
            services.AddScoped<WebsiteBusinessRules>();
            services.AddScoped<OperationClaimBusinessRules>();
            services.AddScoped<UserOperationClaimBusinessRules>();


            services.AddScoped<IAuthService, AuthManager>();


          
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


            return services;

        }
    }
}
