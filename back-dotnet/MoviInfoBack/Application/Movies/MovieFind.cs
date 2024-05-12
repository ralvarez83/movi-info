using Domain.Movies;

namespace Application.Movies
{
  public interface MovieFind
  {
    public Task<Movie> find();
  }
}