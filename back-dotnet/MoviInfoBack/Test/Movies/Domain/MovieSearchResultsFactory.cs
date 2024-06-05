using System.Collections.Immutable;
using AutoFixture;
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
      Fixture fixture = new Fixture();
      return fixture.Create<MovieSearchResults>();
    }
  }
}