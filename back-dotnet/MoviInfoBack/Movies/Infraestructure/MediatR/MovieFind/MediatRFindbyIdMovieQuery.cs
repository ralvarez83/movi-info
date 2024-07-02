using MediatR;
using Movies.Application.Dtos;
using Movies.Application.MovieFind;

namespace Movies.Infraestructure.MediatR.MovieFind
{
  public class MediatRFindbyIdMovieQuery(string movieId) : FindByIdMovieQuery (movieId), IRequest<Movie?>
  {
    
  }
}