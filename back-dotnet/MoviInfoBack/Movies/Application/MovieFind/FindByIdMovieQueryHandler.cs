using Movies.Application.Dtos;
using Movies.Application.Dtos.Transforms;
using MoviesDomain = Movies.Domain.Movie;
using Movies.Domain.ValueObjects;
using Shared.Domain.Bus.Query;

namespace Movies.Application.MovieFind 
{
  public class FindByIdMovieQueryHandler (MovieFindById movieFindById) : QueryHandler <FindByIdMovieQuery, Movie?>
  {
    private MovieFindById _movieFindById = movieFindById;

    public async Task<Movie?> Handle(FindByIdMovieQuery query, CancellationToken cancellationToken)
    {
        MovieId movieId = new MovieId(query.MovieId);

        MoviesDomain? movie = await _movieFindById.Find(movieId);

        return TransformsToMovieDTO.Run(movie);
    }

  }
}