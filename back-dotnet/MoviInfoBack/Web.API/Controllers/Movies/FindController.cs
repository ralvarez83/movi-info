using Movies.Application;
using Movies.Domain.ValueObjects;
using Movies.Infraestructure.TheMovieDb;
using Movies.Infraestructure.TheMovieDb.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Configurations;
using Movie = Movies.Domain.Movie;
using MovieDTO = WebAPI.dto.Movie;

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
    public async Task<ActionResult<MovieDTO>> Get(string id){

      string authorization = _theMovieDBConfiguration.Authorisation;
      Uri baseURL = new Uri(_theMovieDBConfiguration.BaseURL);
      string authorizationType = _theMovieDBConfiguration.AuthorisationType;

      ConfigMovie? repositoryConfig = await ConfigMovie.GetConfig(authorization,baseURL, authorizationType);

      if (null == repositoryConfig)
        return StatusCode(StatusCodes.Status500InternalServerError, repositoryConfig);

      TheMovieDBRepository repository = new TheMovieDBRepository(repositoryConfig);
      MovieId movieIdentificator = new MovieId(id);
      MoviFindById movieFinder = new MoviFindById(repository, movieIdentificator);

      Movie movie = await movieFinder.find();

      if (null == movie)
        return NotFound();

      return MovieDTO.TransformToMovieDTO(movie);
    }
  }
}