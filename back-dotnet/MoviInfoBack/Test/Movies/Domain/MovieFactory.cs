using AutoFixture;
using Domain.Movies;

namespace Test.Movies.Domain
{
  public class MovieFactory
  {
    
    public static Movie BuildRandom(){
      Fixture fixture = new Fixture();
      return fixture.Create<Movie>();
    }

  }
}