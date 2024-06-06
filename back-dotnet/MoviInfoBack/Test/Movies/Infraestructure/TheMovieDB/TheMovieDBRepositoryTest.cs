using System.Collections.Immutable;
using Domain.Movies;
using Domain.Shared.Criteria;
using Domain.Shared.Criteria.Filters;
using Infraestructure.TheMovieDb;
using Infraestructure.TheMovieDb.Entities;
using Test.Movies.Domain;
using Test.Movies.Domain.ValueObjects;
using Test.Movies.Infraestructure.TheMovieDB.Factories;
using WebAPI.Configurations;
using Movie = Domain.Movies.Movie;

namespace Test.Movies.Infraestructure.TheMovieDB
{
  [TestClass]
  public class TheMovieDBRepositoryTest
 {
    [TestMethod]
    public async Task SearchMoviesWithBadConfigShouldReturnEmptyMovieListAndSamePagination (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(config);

      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieSearchResults moviResults = await moviRepo.searchByCriteria(criteria);

      MovieSearchResults movieResultExpected = new MovieSearchResults([], criteria.pagination);

      Assert.AreSame(movieResultExpected.movies, moviResults.movies);
      Assert.AreSame(movieResultExpected.pagination, moviResults.pagination);
    }

    [TestMethod]
    public async Task SearchMoviesWithoutFiltersShouldReturn20Movies (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(config);

      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      // When
      MovieSearchResults movieResults = await moviRepo.searchByCriteria(criteria);

      // Then

      Assert.AreEqual(20, movieResults.movies.Count);
      Assert.AreEqual(criteria.pagination.page, movieResults.pagination.page);
    }


    [TestMethod]
    public async Task SearchMoviesWithTextFilterShouldReturnMoviesWithThisTextInTitleOrOverview (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(config);

      string spanishTextFilter = "casa";
      string englishTextFilter = "home";

      Filters filters = FiltersFactory.BuildSpecificTextFilter(spanishTextFilter);

      Criteria criteria = CriteriaFactory.BuildWithInitialPagination(filters);

      // When
      MovieSearchResults movieResults = await moviRepo.searchByCriteria(criteria);

      // Then
      movieResults.movies.ForEach(movie => {
        Assert.IsTrue(
          movie.title.ToLower().Contains(spanishTextFilter.ToLower()) || 
          movie.overview.ToLower().Contains(spanishTextFilter.ToLower()) || 
          movie.title.ToLower().Contains(englishTextFilter.ToLower()) || 
          movie.overview.ToLower().Contains(englishTextFilter.ToLower()) 
        );
      });      
    }


    [TestMethod]
    public async Task FindMoviesWithBadConfigShouldReturnEmptyMovie (){
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildBadAuthorisationOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(config);


      Movie? movieReturn = await moviRepo.findById(MovieIdFactory.BuildRandomMovieID());

      Assert.IsNull(movieReturn);
    }

    [TestMethod]
    public async Task FindMovieByBadId (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(config);

      Movie? movieReturn = await moviRepo.findById(MovieIdFactory.BuildNoExistsMovieID());

      Assert.IsNull(movieReturn);
    }

    [TestMethod]
    public async Task FindMovieByRightId (){
      // Given
      TheMovieDBOptions theMovieDBOptions = TheMovieDBOptionsFactory.BuildRigthOptions();
      Uri baseURL = new Uri(theMovieDBOptions.BaseURL);
      
      ConfigMovie? config = await ConfigMovie.GetConfig(theMovieDBOptions.Authorisation,baseURL,theMovieDBOptions.AuthorisationType);

      TheMovieDBRepository moviRepo = new TheMovieDBRepository(config);

      Movie? movieReturn = await moviRepo.findById(MovieIdFactory.BuildExistsMovieID());

      Assert.IsNotNull(movieReturn);
    }
  }
}