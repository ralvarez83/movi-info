using Movies.Domain;

namespace Movies.Application
{
  public interface MovieFind
  {
    public Task<Movie> find();
  }
}