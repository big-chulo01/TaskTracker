using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Services;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Infrastructure.Repositories;

namespace TaskTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, InMemoryTaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}