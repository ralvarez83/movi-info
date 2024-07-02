using Movies.Application.MovieFind;
using Movies.Infraestructure.TheMovieDb;
using Microsoft.AspNetCore.Mvc;
using Movie = Movies.Application.Dtos.Movie;
using Movies.Domain;
using MediatR;
using Movies.Infraestructure.MediatR.MovieFind;

namespace WebAPI.Controllers.Movies
{
  
  [ApiController]
  [Route("api/movies/")]
  public class FindController (Mediator mediator) :ControllerBase
  {
    private readonly Mediator _mediator = mediator;
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(string id){

      MediatRFindbyIdMovieQuery query = new MediatRFindbyIdMovieQuery(id);

      Movie? movie = await _mediator.Send(query);

      if (null == movie)
        return NotFound();

      return movie;
    }
  }
}