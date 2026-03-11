using Microsoft.Extensions.DependencyInjection;
using Recruitment.Interfaces;
using Recruitment.Sdk.Clients;

namespace Recruitment.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Recruitment SDK typed HTTP clients using IHttpClientFactory.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="apiBaseAddress">The base address of the Recruitment API.</param>
        public static IServiceCollection AddRecruitmentSdk(
            this IServiceCollection services,
            string apiBaseAddress)
        {
            services.AddHttpClient<ICandidateService, CandidateClient>(client =>
                client.BaseAddress = new Uri(apiBaseAddress));

            services.AddHttpClient<IJobService, JobClient>(client =>
                client.BaseAddress = new Uri(apiBaseAddress));

            services.AddHttpClient<IRecruiterService, RecruiterClient>(client =>
                client.BaseAddress = new Uri(apiBaseAddress));

            services.AddHttpClient<ISkillService, SkillClient>(client =>
                client.BaseAddress = new Uri(apiBaseAddress));

            return services;
        }
    }
}
