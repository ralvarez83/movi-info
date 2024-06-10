using Movies.Application;
using Movies.Domain;
using Shared.Domain.Criteria;
using Moq;
using Test.Movies.Domain;
using Movies.Application.MovieSearch;
using MovieSearchResultsDTO = Movies.Application.DTO.MovieSearchResults;
using Movies.Application.DTO.Transforms;

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

      MovieSearchResultsDTO movieResultsResponse = await movieSearcher.search();
      
      Assert.Equal(TransformsToMovieSearchResultsDTO.Run(movieResult), movieResultsResponse);
    }


    [Fact]
    public async Task SearchAListWithData(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildRandomListMovies();
      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok, criteria);

      MovieSearchResultsDTO movieResultsResponse = await movieSearcher.search();
      MovieSearchResultsDTO movieSearchExpected = TransformsToMovieSearchResultsDTO.Run(movieResult);
      
      Assert.Same(movieSearchExpected.pagination, movieResultsResponse.pagination);
      Assert.Equal(movieSearchExpected.movies, movieResultsResponse.movies);
    }    
  }
}