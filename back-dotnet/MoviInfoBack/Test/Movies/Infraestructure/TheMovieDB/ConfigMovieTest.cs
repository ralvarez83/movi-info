using Infraestructure.TheMovieDb.Entities;
using Test.Movies.Infraestructure.TheMovieDB.Factories;
using WebAPI.Configurations;

namespace Test.Movies.Infraestructure.TheMovieDB
{
  public class ConfigMovieTest
  {
    [Fact]
    public async Task RecoveringConfig (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);

      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.NotNull(config);
    }
    
    [Fact]
    public async Task RecoveringConfigWithoutAuthorisationShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.Null(config);
    }
    
    [Fact]
    public async Task RecoveringConfigWithBadURLShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadBaseURLOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.Null(config);
    }
    
    [Fact]
    public async Task RecoveringConfigWithBadAuthorisationTypeShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationTypeOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      Assert.Null(config);
    }
  }
}