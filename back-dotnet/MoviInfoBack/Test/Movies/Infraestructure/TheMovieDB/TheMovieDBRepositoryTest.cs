using Movies.Domain;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;
using Movies.Infraestructure.TheMovieDb;
using Movies.Infraestructure.TheMovieDb.Entities;
using Test.Movies.Domain;
using Test.Movies.Domain.ValueObjects;
using Test.Movies.Infraestructure.TheMovieDB.Factories;
using Movie = Movies.Domain.Movie;
using Movies.Infraestructure.TheMovieDb.Configuration;
using Microsoft.Extensions.Options;

namespace Test.Movies.Infraestructure.TheMovieDB
{
  public class TheMovieDBRepositoryTest
 {
    [Fact]
    public async Task SearchMoviesWithBadConfigShouldReturnEmptyMovieListAndSamePagination (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(configRepository);

      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieSearchResults moviResults = await moviRepo.searchByCriteria(criteria);

      MovieSearchResults movieResultExpected = new MovieSearchResults([], criteria.pagination);

      Assert.Same(movieResultExpected.movies, moviResults.movies);
      Assert.Same(movieResultExpected.pagination, moviResults.pagination);
    }

    [Fact]
    public async Task SearchMoviesWithoutFiltersShouldReturn20Movies (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(configRepository);

      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      // When
      MovieSearchResults movieResults = await moviRepo.searchByCriteria(criteria);

      // Then

      Assert.Equal(20, movieResults.movies.Count);
      Assert.Equal(criteria.pagination.page, movieResults.pagination.page);
    }


    [Fact]
    public async Task SearchMoviesWithTextFilterShouldReturnMoviesWithThisTextInTitleOrOverview (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(configRepository);

      string spanishTextFilter = "casa";
      string englishTextFilter = "home";

      Filters filters = FiltersFactory.BuildSpecificTextFilter(spanishTextFilter);

      Criteria criteria = CriteriaFactory.BuildWithInitialPagination(filters);

      // When
      MovieSearchResults movieResults = await moviRepo.searchByCriteria(criteria);

      // Then
      movieResults.movies.ForEach(movie => {
        Assert.True(
          movie.title.ToLower().Contains(spanishTextFilter.ToLower()) || 
          movie.overview.ToLower().Contains(spanishTextFilter.ToLower()) || 
          movie.title.ToLower().Contains(englishTextFilter.ToLower()) || 
          movie.overview.ToLower().Contains(englishTextFilter.ToLower()) 
        );
      });      
    }


    [Fact]
    public async Task FindMoviesWithBadConfigShouldReturnEmptyMovie (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(configRepository);


      Movie? movieReturn = await moviRepo.findById(MovieIdFactory.BuildRandomMovieID());

      Assert.Null(movieReturn);
    }

    [Fact]
    public async Task FindMovieByBadId (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(configRepository);

      Movie? movieReturn = await moviRepo.findById(MovieIdFactory.BuildNoExistsMovieID());

      Assert.Null(movieReturn);
    }

    [Fact]
    public async Task FindMovieByRightId (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      
      IOptions<TheMovieDBOptions> options = Options.Create<TheMovieDBOptions>(theMovieDBOptions);

      ConfigTheMovieDBRespository configRepository = new ConfigTheMovieDBRespository(options);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(configRepository);

      Movie? movieReturn = await moviRepo.findById(MovieIdFactory.BuildExistsMovieID());

      Assert.NotNull(movieReturn);
    }
  }
}