using Movies.Application.MovieFind;
using Movies.Infraestructure.TheMovieDb;
using Microsoft.AspNetCore.Mvc;
using Movie = Movies.Application.Dtos.Movie;
using Movies.Domain;
using MediatR;
using Movies.Infraestructure.MediatR.MovieFind;
using Shared.Domain.Bus.Query;

namespace WebAPI.Controllers.Movies
{
  
  [ApiController]
  [Route("api/movies/")]
  public class FindController (QueryBus bus) :ControllerBase
  {
    private readonly QueryBus _bus = bus;
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(string id){

      MediatRFindbyIdMovieQuery query = new MediatRFindbyIdMovieQuery(id);

      Movie? movie = await _bus.Ask<Movie?>(query);

      if (null == movie)
        return NotFound();

      return movie;
    }
  }
}