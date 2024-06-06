using Infraestructure.TheMovieDb.Entities;
using Test.Movies.Infraestructure.TheMovieDB.Factories;
using WebAPI.Configurations;

namespace Test.Movies.Infraestructure.TheMovieDB
{
  [TestClass]
  public class ConfigMovieTest
  {
    [TestMethod]
    public async Task RecoveringConfig (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);

      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.IsNotNull(config);
    }
    
    [TestMethod]
    public async Task RecoveringConfigWithoutAuthorisationShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.IsNull(config);
    }
    
    [TestMethod]
    public async Task RecoveringConfigWithBadURLShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadBaseURLOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.IsNull(config);
    }
    
    [TestMethod]
    public async Task RecoveringConfigWithBadAuthorisationTypeShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationTypeOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.IsNull(config);
    }
  }
}