using Movies.Application.MovieFind;
using Movies.Domain.ValueObjects;
using Movies.Infraestructure.TheMovieDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Configurations;
using Movies.Infraestructure.TheMovieDb.Entities;
using Movie = Movies.Application.DTO.Movie;
using Movies.Application.DTO.Transforms;

namespace WebAPI.Controllers.Movies
{
  
  [ApiController]
  [Route("api/movie/")]
  public class FindController:ControllerBase
  {
    private readonly TheMovieDBOptions _theMovieDBConfiguration;
    public FindController(IOptions<TheMovieDBOptions> options){
      _theMovieDBConfiguration = options.Value;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(string id){

      string authorization = _theMovieDBConfiguration.Authorisation;
      Uri baseURL = new Uri(_theMovieDBConfiguration.BaseURL);
      string authorizationType = _theMovieDBConfiguration.AuthorisationType;

      ConfigMovie? repositoryConfig = await ConfigMovie.GetConfig(authorization,baseURL, authorizationType);

      if (null == repositoryConfig)
        return StatusCode(StatusCodes.Status500InternalServerError, repositoryConfig);

      TheMovieDBRepository repository = new TheMovieDBRepository(repositoryConfig);
      MoviFindById movieFinder = new MoviFindById(repository);

      FindByIdMovieQuery query = new FindByIdMovieQuery(id);
      FindByIdMovieQueryHandler handler = new FindByIdMovieQueryHandler(movieFinder);

      Movie? movie = await handler.Run(query);

      if (null == movie)
        return NotFound();

      return movie;
    }
  }
}