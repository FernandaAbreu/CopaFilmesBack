using System;
using System.Collections.Generic;
using CopaFilmesBackEnd.ViewModel;

namespace CopaFilmesBackEnd.Services.Interfaces
{
    public interface IMovieService
    {
        public string GetAll();
        public List<VMMovie> generatePositionsBySomeMovies(List<VMMovie> list);
    }
}
