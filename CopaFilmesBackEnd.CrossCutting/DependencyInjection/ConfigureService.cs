using System;
using CopaFilmesBackEnd.Services;
using CopaFilmesBackEnd.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmesBackEnd.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMovieService, MovieService>();
        }
    }
}
