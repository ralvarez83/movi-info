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
    public async Task FindEmptyList(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildEmptyListMovies();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(It.IsAny<Criteria>()) == Task<MovieSearchResults>.FromResult(movieResult));

      Criteria criteria = CriteriaFactory.BuildRandom();

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok, criteria);

      MovieSearchResults movieResultsResponse = await movieSearcher.search();
      
      Assert.AreSame(movieResult, movieResultsResponse);
    }


    [TestMethod]
    public async Task FindList(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildEmptyListMovies();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(It.IsAny<Criteria>()) == Task<MovieSearchResults>.FromResult(movieResult));

      Criteria criteria = CriteriaFactory.BuildRandom();

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok, criteria);

      MovieSearchResults movieResultsResponse = await movieSearcher.search();
      
      Assert.AreSame(movieResult, movieResultsResponse);
    }
    
  }
}