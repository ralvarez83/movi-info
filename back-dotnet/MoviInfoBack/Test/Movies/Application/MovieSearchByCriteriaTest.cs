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

      MovieSearchResultsDTO movieResultsResponse = await movieSearcher.Search(criteria);
      
      Assert.Equal(TransformsToMovieSearchResultsDTO.Run(movieResult), movieResultsResponse);
    }


    [Fact]
    public async Task SearchAListWithData(){
      MovieSearchResults movieResult = MovieSearchResultsFactory.BuildRandomListMovies();
      Criteria criteria = CriteriaFactory.BuildWithRandomPaginationEmptyFilters();

      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.searchByCriteria(criteria) == Task<MovieSearchResults>.FromResult(movieResult));

      MovieSearchByCriteria movieSearcher = new MovieSearchByCriteria(moviRepoMok);

      MovieSearchResultsDTO movieResultsResponse = await movieSearcher.Search(criteria);
      MovieSearchResultsDTO movieSearchExpected = TransformsToMovieSearchResultsDTO.Run(movieResult);
      
      Assert.Same(movieSearchExpected.pagination, movieResultsResponse.pagination);
      Assert.Equal(movieSearchExpected.movies, movieResultsResponse.movies);
    }    
  }
}