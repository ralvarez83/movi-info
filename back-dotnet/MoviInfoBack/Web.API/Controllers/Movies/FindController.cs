using Movies.Application.MovieFind;
using Movies.Infraestructure.TheMovieDb;
using Microsoft.AspNetCore.Mvc;
using Movie = Movies.Application.Dtos.Movie;
using Movies.Domain;

namespace WebAPI.Controllers.Movies
{
  
  [ApiController]
  [Route("api/movies/")]
  public class FindController (MovieRepository repository) :ControllerBase
  {
    private readonly TheMovieDBRepository _repository = (TheMovieDBRepository) repository;
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(string id){

      MoviFindById movieFinder = new MoviFindById(_repository);

      FindByIdMovieQuery query = new FindByIdMovieQuery(id);
      FindByIdMovieQueryHandler handler = new FindByIdMovieQueryHandler(movieFinder);

      Movie? movie = await handler.Run(query);

      if (null == movie)
        return NotFound();

      return movie;
    }
  }
}