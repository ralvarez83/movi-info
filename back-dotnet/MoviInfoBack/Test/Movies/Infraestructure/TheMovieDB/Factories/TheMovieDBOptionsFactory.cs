using Movies.Infraestructure.TheMovieDb.Configuration;

namespace Test.Movies.Infraestructure.TheMovieDB.Factories
{
  public class TheMovieDBOptionsFactory
  {
    public static TheMovieDBOptions BuildRigthOptions (){
      return new TheMovieDBOptions(){
        Authorisation = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs",
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