using Application.Movies;
using Domain.Movies;
using Domain.Shared.Criteria;
using Moq;
using Test.Movies.Domain;

namespace Test.Movies.Application
{
  [TestClass]
  public class MovieSearchByCriteriaTest
  {

    [TestMethod]
    public async Task SearchEmptyList(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildEmptyListMovies();

      Criteria criteria = CriteriaFactory.BuildRandom();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok, criteria);

      MovieSearchResults movieResultsResponse = await movieSearcher.search();
      
      Assert.AreSame(movieResult, movieResultsResponse);
    }


    [TestMethod]
    public async Task SearchAListWithData(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildRandomListMovies();
      Criteria criteria = CriteriaFactory.BuildRandom();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok, criteria);

      MovieSearchResults movieResultsResponse = await movieSearcher.search();
      
      Assert.AreSame(movieResult, movieResultsResponse);
    }    
  }
}