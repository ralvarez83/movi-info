using Movies.Infraestructure.TheMovieDb.Configuration;

namespace Test.Movies.Infraestructure.TheMovieDB.Factories
{
  public class TheMovieDBOptionsFactory
  {
    // Las pruebas de infraestructura atacan la API real de TheMovieDB, así que necesitan un token
    // válido. Se lee del entorno para no versionarlo: TheMovieDB__Authorisation.
    public static TheMovieDBOptions BuildRigthOptions (){
      string? authorisation = Environment.GetEnvironmentVariable("TheMovieDB__Authorisation");

      if (String.IsNullOrWhiteSpace(authorisation)){
        throw new InvalidOperationException(
          "Las pruebas de infraestructura necesitan un token real de TheMovieDB. " +
          "Defina la variable de entorno 'TheMovieDB__Authorisation' antes de ejecutarlas.");
      }

      return new TheMovieDBOptions(){
        Authorisation = authorisation,
        AuthorisationType = "Bearer",
        BaseURL = "https://api.themoviedb.org/3/"
      };
    }
    public static TheMovieDBOptions BuildBadAuthorisationOptions (){
      TheMovieDBOptions theMovieDBOptions = BuildRigthOptions();
      theMovieDBOptions.Authorisation = "Bad Authorization";
      
      return theMovieDBOptions;
    }
    public static TheMovieDBOptions BuildBadBaseURLOptions (){
      TheMovieDBOptions theMovieDBOptions = BuildRigthOptions();
      theMovieDBOptions.BaseURL = "https://google.com/";
      
      return theMovieDBOptions;
    }
    public static TheMovieDBOptions BuildBadAuthorisationTypeOptions (){
      TheMovieDBOptions theMovieDBOptions = BuildRigthOptions();
      theMovieDBOptions.AuthorisationType = "AA";
      
      return theMovieDBOptions;
    }
  }
}