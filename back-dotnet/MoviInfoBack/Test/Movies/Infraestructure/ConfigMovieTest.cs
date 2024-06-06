using Infraestructure.TheMovieDb.Entities;

namespace Test.Movies.Infraestructure
{
  [TestClass]
  public class ConfigMovieTest
  {
    [TestMethod]
    public async Task RecoveringConfig (){
      string authorisation = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs";
      string authorisationType = "Bearer";
      Uri baseURL = new Uri("https://api.themoviedb.org/3/");
      ConfigMovie? config = await ConfigMovie.GetConfig(authorisation,baseURL,authorisationType);

      Assert.IsNotNull(config);
    }
    
    [TestMethod]
    public async Task RecoveringConfigWithoutAuthorisationShouldBeNull (){
      string authorisation = "Not Valid";
      string authorisationType = "Bearer";
      Uri baseURL = new Uri("https://api.themoviedb.org/3/");
      ConfigMovie? config = await ConfigMovie.GetConfig(authorisation,baseURL,authorisationType);

      Assert.IsNull(config);
    }
    
    [TestMethod]
    public async Task RecoveringConfigWithBadURLShouldBeNull (){
      string authorisation = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs";
      string authorisationType = "Bearer";
      Uri baseURL = new Uri("https://google.com/");
      ConfigMovie? config = await ConfigMovie.GetConfig(authorisation,baseURL,authorisationType);

      Assert.IsNull(config);
    }
    
    [TestMethod]
    public async Task RecoveringConfigWithBadAuthorisationTypeShouldBeNull (){
      string authorisation = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs";
      string authorisationType = "AA";
      Uri baseURL = new Uri("https://api.themoviedb.org/3/");
      ConfigMovie? config = await ConfigMovie.GetConfig(authorisation,baseURL,authorisationType);

      Assert.IsNull(config);
    }
  }
}