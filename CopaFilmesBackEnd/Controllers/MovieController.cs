
using System;
using System.Collections.Generic;
using CopaFilmesBackEnd.Extensions;
using CopaFilmesBackEnd.Helpers;
using CopaFilmesBackEnd.Services.Interfaces;
using CopaFilmesBackEnd.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace CopaFilmesBackEnd.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        IMovieService mMovieService;
        private readonly ILogger logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            mMovieService = movieService;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VMMovie>> GetAll()
        {
            try
            {
                var movies = mMovieService.GetAll();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                logger.LogError("Internal Error" + ex.Message);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }



        [HttpPost]
        public ActionResult<List<VMMovie>> Post([FromBody] List<VMMovie> listMovies)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorMessages());
                }

                return mMovieService.generatePositionsBySomeMovies(listMovies);
            }
            catch (CustomHttpException ex)
            {
                logger.LogError("Exception expected" + ex.Message);
                return StatusCode(ex.StatusCode, ex.ErrorMessage);
            }
            catch (Exception ex)
            {
                logger.LogError("Internal Error" + ex.Message);
                return StatusCode(500, new { error = "Internal server error" });
            }

        }
    }
}
