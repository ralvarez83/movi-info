using MediatR;
using Movies.Application.Dtos;
using Movies.Application.MovieSearch;
using Shared.Domain.Criteria;

namespace Movies.Infraestructure.MediatR.MovieSearch
{
  public class MediatRMovieSearchByCriteriaQuery(Criteria criteria) : MovieSearchByCriteriaQuery (criteria), IRequest<MovieSearchResults>
  {
    
  }
}