using System.Collections.Immutable;
using System.Net.Http.Headers;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using Domain.Shared.Criteria;
using Domain.Shared.Criteria.Filters;
using Domain.Shared.Criteria.Order;
using Infraestructure.TheMovieDb.Entities;
using MovieDomain = Domain.Movies.Movie;


namespace Infraestructure.TheMovieDb
{
    public class TheMovieDBRepository : MovieRepository
    {

      private readonly ConfigMovie _config;

      public TheMovieDBRepository(ConfigMovie config) => _config = config;

      public async Task<MovieDomain?> findById(MovieId movieId)
      {
        
        TheMovieDBCriteriaTransformation criteriaTransformation = new TheMovieDBCriteriaTransformation();
        string apiURL = ConfigMovie.MOVIE_FIND + movieId.value + criteriaTransformation.getEmptyCriteria() ; 

        HttpResponseMessage response = await _config.Client.GetAsync(apiURL);

        if (!response.IsSuccessStatusCode)
          return null;

        Entities.Movie movie = await response.Content.ReadAsAsync<Entities.Movie>();
        return movie.toMovieDomain(_config.images.base_url + _config.images.backdrop_sizes[2]);
      }

      public async Task<MovieSearchResults> searchByCriteria(Criteria criteria)
      {
        TheMovieDBCriteriaTransformation criteriaTransformation = new TheMovieDBCriteriaTransformation(criteria);

        string queryType = criteriaTransformation.isSearch() ? ConfigMovie.SEARCH : ConfigMovie.DISCOVER;

        string apiURL = queryType + criteriaTransformation.getCriterias(); 

        HttpResponseMessage response = await _config.Client.GetAsync(apiURL);

        if (!response.IsSuccessStatusCode)
          return new MovieSearchResults(ImmutableList<MovieDomain>.Empty, criteriaTransformation.getPagination());
        
        MovieAPIResult apiResults = await response.Content.ReadAsAsync<MovieAPIResult>();

        ImmutableList<MovieDomain> movies = (from movie in apiResults.results
          select movie.toMovieDomain(_config.images.base_url + _config.images.backdrop_sizes[2])).ToImmutableList();
        
        return new MovieSearchResults(movies, new Pagination(apiResults.page, apiResults.total_pages));

      }

    }
}