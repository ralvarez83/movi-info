using Movies.Application;
using Movies.Domain;
using Shared.Domain.Criteria;
using Moq;
using Test.Movies.Domain;
using Movies.Application.MovieSearch;
using MovieSearchResultsDTO = Movies.Application.Dtos.MovieSearchResults;
using Movies.Application.Dtos.Transforms;

namespace Test.Movies.Application
{
  public class MovieSearchByCriteriaTest
  {

    [Fact]
    public async Task SearchEmptyList(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildEmptyListMovies();

      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok);

      MovieSearchResults movieResultsResponse = await movieSearcher.Search(criteria);
      
      Assert.Equal(movieResult, movieResultsResponse);
    }


    [Fact]
    public async Task SearchAListWithData(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildRandomListMovies();
      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok);

      MovieSearchResults movieResultsResponse = await movieSearcher.Search(criteria);
      
      Assert.Same(movieResult.pagination, movieResultsResponse.pagination);
      Assert.Equal(movieResult.movies, movieResultsResponse.movies);
    }    
  }
}