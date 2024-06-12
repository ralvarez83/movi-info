using Movies.Application.DTO.Transforms;
using Movies.Domain;
using Movies.Domain.ValueObjects;
using Shared.Domain.Bus.Query;

namespace Movies.Application.MovieFind
{
  public sealed class FindByIdMovieQueryHandler : QueryHandler
  {
    private MoviFindById _movieFinder;
    public FindByIdMovieQueryHandler (MoviFindById movieFinder){
      this._movieFinder = movieFinder;
    }

    public async Task<DTO.Movie?> Run(FindByIdMovieQuery query)
    {
        MovieId movieId = new MovieId(query.movieId);

        Movie? movie = await this._movieFinder.find(movieId);
        return TransformsToMovieDTO.Run(movie);
    }
 }
}