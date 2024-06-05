using Domain.Movies.ValueObjects;

namespace Test.Movies.Domain.ValueObjects
{
  public class MovieIdFactory
  {
    public static MovieId MovieIDRandomBuild(){
      return new MovieId(Guid.NewGuid().ToString());
    }
  }
}