using Movies.Application;
using Movies.Domain;
using Shared.Domain.Criteria;
using Moq;
using Test.Movies.Domain;

namespace Test.Movies.Application
{
  public class MovieSearchByCriteriaTest
  {

    [Fact]
    public async Task SearchEmptyList(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildEmptyListMovies();

      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok, criteria);

      MovieSearchResults movieResultsResponse = await movieSearcher.search();
      
      Assert.Same(movieResult, movieResultsResponse);
    }


    [Fact]
    public async Task SearchAListWithData(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildRandomListMovies();
      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok, criteria);

      MovieSearchResults movieResultsResponse = await movieSearcher.search();
      
      Assert.Same(movieResult, movieResultsResponse);
    }    
  }
}