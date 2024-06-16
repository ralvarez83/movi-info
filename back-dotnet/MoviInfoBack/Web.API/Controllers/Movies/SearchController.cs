using Movies.Application.MovieSearch;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;
using Movies.Infraestructure.TheMovieDb;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Dtos;
using Movies.Domain;
using MovieSearchResults = Movies.Application.Dtos.MovieSearchResults;

namespace WebAPI.Controllers.Movies
{
 
[ApiController]
[Route("api/movies/")]
public class SearchController (MovieRepository repository) :ControllerBase
  {
    private readonly TheMovieDBRepository _repository = (TheMovieDBRepository) repository;
    
  [HttpGet]
  public async Task<ActionResult<MovieSearchResults>> Get(string? byText, int page, int totalPages){

    Filters filters = new Filters();
    if (!String.IsNullOrEmpty(byText)){
      Filter textFilter = new Filter(Filter.FILTER_BY_TEXT, byText, FilterOperator.CONTAINS);
      filters.add(textFilter);
    }

    Pagination pagination =new Pagination(page, totalPages);

    Criteria criteria = new Criteria(filters,pagination);

    MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria (_repository,criteria);

    MovieSearchResults movieSearchResults = await movieSearcher.Search();

    return movieSearchResults;
  }


}


}
