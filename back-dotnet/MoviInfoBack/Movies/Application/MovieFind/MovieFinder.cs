using Movies.Application.DTO;

namespace Movies.Application.MovieFind
{
  public interface MovieFinder
  {
    public Task<Movie?> find();
  }
}