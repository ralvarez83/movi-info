using System.Collections.Immutable;
using Domain.Movies;
using Domain.Shared.Criteria;

namespace Test.Movies.Domain
{
  public class MovieSearchResultsFactory
  {
    public static MovieSearchResults BuildEmptyListMovies(){
      ImmutableList<Movie> movies = [];
      Pagination pagination = PaginationFactory.BuildInitial();


      MovieSearchResults movieResults = new MovieSearchResults(movies, pagination);
      return movieResults;
    }

    public static MovieSearchResults BuildRandomListMovies(){
      ImmutableList<Movie> movies = MovieFactory.BuildRandomList();
      Pagination pagination = PaginationFactory.BuildInitial();
      return new MovieSearchResults(movies, pagination);
    }
  }
}