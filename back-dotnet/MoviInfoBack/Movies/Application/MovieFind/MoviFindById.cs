using Movies.Domain;
using Movies.Domain.ValueObjects;

namespace Movies.Application.MovieFind
{
    public class MoviFindById
    {
      private MovieRepository _repository;
      public MoviFindById(MovieRepository repository)
      {
        _repository = repository;
      }

      public async Task<Movie?> Find(MovieId movieId)
      {
          return await _repository.findById(movieId);
      }
    }
}