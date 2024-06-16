using Movies.Application.Dtos.Transforms;
using Movies.Domain;
using Movies.Domain.ValueObjects;
using Shared.Domain.Bus.Query;

namespace Movies.Application.MovieFind 
{
  public sealed class FindByIdMovieQueryHandler (MoviFindById movieFinder) : QueryHandler
  {
    public async Task<Dtos.Movie?> Run(FindByIdMovieQuery query)
    {
        MovieId movieId = new MovieId(query.movieId);

        Movie? movie = await movieFinder.Find(movieId);
        return TransformsToMovieDTO.Run(movie);
    }
 }
}