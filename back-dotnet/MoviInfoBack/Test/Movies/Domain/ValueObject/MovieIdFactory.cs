using Movies.Domain.ValueObjects;

namespace Test.Movies.Domain.ValueObjects
{
  public class MovieIdFactory
  {
    public static MovieId BuildRandomMovieID(){
      return new MovieId(Guid.NewGuid().ToString());
    }

    public static MovieId BuildNoExistsMovieID(){
      return new MovieId("-3");
    }
    public static MovieId BuildExistsMovieID(){
      return new MovieId("1216299");
    }
  }
}