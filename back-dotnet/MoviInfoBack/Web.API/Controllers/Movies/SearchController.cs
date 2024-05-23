using Application.Movies;
using Domain.Movies;
using Domain.Shared.Criteria;
using Domain.Shared.Criteria.Filters;
using Infraestructure.TheMovieDb;
using Infraestructure.TheMovieDb.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Configurations;
using MovieSearchResultDTO = WebAPI.dto.MovieSearchResults;

namespace WebAPI.Controllers.Movies
{
 
[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
  private readonly TheMovieDBOptions _theMovieDBConfiguration;
  public SearchController(IOptions<TheMovieDBOptions> options){
    _theMovieDBConfiguration = options.Value;
  }

  [HttpGet]
  public async Task<ActionResult<MovieSearchResultDTO>> Get(string? byText, int page, int totalPages){
    string authorization = _theMovieDBConfiguration.Authorization;
    Uri baseURL = new Uri(_theMovieDBConfiguration.BaseURL);
    string authorizationType = _theMovieDBConfiguration.AuthorizationType;

    ConfigMovie? repositoryConfig = await ConfigMovie.GetConfig(authorization,baseURL, authorizationType);

    if (null == repositoryConfig)
      return StatusCode(StatusCodes.Status500InternalServerError, repositoryConfig);

    TheMovieDBRepository repository = new TheMovieDBRepository(repositoryConfig);

    Filters filters = new Filters();
    if (!String.IsNullOrEmpty(byText)){
      Filter textFilter = new Filter(Filter.FILTER_BY_TEXT, byText, FilterOperator.CONTAINS);
      filters.add(textFilter);
    }

    Pagination pagination =new Pagination(page, totalPages);

    Criteria criteria = new Criteria(filters,pagination);

    MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria (repository,criteria);

    MovieSearchResults movieSearchResults = await movieSearcher.search();

    MovieSearchResultDTO movieSearchResultsDTO = new MovieSearchResultDTO(movieSearchResults);
    return movieSearchResultsDTO;
  }


}


}
