using System.Collections.Immutable;
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

    public static ImmutableList<Movie> BuildRandomList(){
      Fixture fixture = new Fixture();

      return fixture.CreateMany<Movie>().ToImmutableList<Movie>();
    }

  }
}