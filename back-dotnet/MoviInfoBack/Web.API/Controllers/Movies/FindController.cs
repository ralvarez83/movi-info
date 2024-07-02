using Movies.Application.MovieFind;
using Movies.Infraestructure.TheMovieDb;
using Microsoft.AspNetCore.Mvc;
using Movie = Movies.Application.Dtos.Movie;
using Movies.Domain;

namespace WebAPI.Controllers.Movies
{
  
  [ApiController]
  [Route("api/movies/")]
  public class FindController (MoviFindById movieFinder) :ControllerBase
  {
    private readonly MoviFindById _movieFinder = movieFinder;
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(string id){

      FindByIdMovieQuery query = new FindByIdMovieQuery(id);
      FindByIdMovieQueryHandler handler = new FindByIdMovieQueryHandler(_movieFinder);

      Movie? movie = await handler.Run(query);

      if (null == movie)
        return NotFound();

      return movie;
    }
  }
}