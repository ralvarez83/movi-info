using Movies.Domain;
using Movies.Domain.ValueObjects;

namespace Movies.Application.MovieFind
{
    public class MovieFindById
    {
      private MovieRepository _repository;
      public MovieFindById(MovieRepository repository)
      {
        _repository = repository;
      }

      public async Task<Movie?> Find(MovieId movieId)
      {
          return await _repository.findById(movieId);
      }
    }
}