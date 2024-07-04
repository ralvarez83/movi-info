using Movies.Application.MovieSearch;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;
using Movies.Infraestructure.TheMovieDb;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Dtos;
using Movies.Domain;
using MovieSearchResults = Movies.Application.Dtos.MovieSearchResults;
using MediatR;
using Movies.Infraestructure.MediatR.MovieSearch;
using Shared.Domain.Bus.Query;

namespace WebAPI.Controllers.Movies
{
 
[ApiController]
[Route("api/movies/")]
public class SearchController (QueryBus bus) :ControllerBase
{
    private readonly QueryBus _bus = bus;
    
  [HttpGet]
  public async Task<ActionResult<MovieSearchResults>> Get(string? byText, int page, int totalPages){

    Filters filters = new Filters();
    if (!string.IsNullOrEmpty(byText)){
      Filter textFilter = new Filter(Filter.FILTER_BY_TEXT, byText, FilterOperator.CONTAINS);
      filters.add(textFilter);
    }

    Pagination pagination =new Pagination(page, totalPages);

    Criteria criteria = new Criteria(filters,pagination);

    MediatRMovieSearchByCriteriaQuery query = new MediatRMovieSearchByCriteriaQuery(criteria);

    MovieSearchResults movieSearchResults = await _bus.Ask<MovieSearchResults>(query);

    return movieSearchResults;
  }


}


}
