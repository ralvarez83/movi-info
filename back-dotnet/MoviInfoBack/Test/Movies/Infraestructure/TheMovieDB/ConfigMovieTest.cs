using Microsoft.Extensions.Options;
using Movies.Infraestructure.TheMovieDb.Configuration;
using Test.Movies.Infraestructure.TheMovieDB.Factories;

namespace Test.Movies.Infraestructure.TheMovieDB
{
  public class ConfigMovieTest
  {
    [Fact]
    public async Task RecoveringConfig (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      ConfigMovie? configMovie = await configRepository.GetConfigMovies();

      Assert.NotNull(configMovie);
    }
    
    [Fact]
    public async Task RecoveringConfigWithoutAuthorisationShouldBeNull (){      
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      ConfigMovie? configMovie = await configRepository.GetConfigMovies();

      Assert.Null(configMovie);
    }
    
    [Fact]
    public async Task RecoveringConfigWithBadURLShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadBaseURLOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      ConfigMovie? configMovie = await configRepository.GetConfigMovies();
      
      Assert.Null(configMovie);
    }
    
    [Fact]
    public async Task RecoveringConfigWithBadAuthorisationTypeShouldBeNull (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationTypeOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      ConfigMovie? configMovie = await configRepository.GetConfigMovies();
      
      Assert.Null(configMovie);
    }
  }
}