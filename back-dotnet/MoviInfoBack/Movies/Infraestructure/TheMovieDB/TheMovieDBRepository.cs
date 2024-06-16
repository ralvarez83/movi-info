using System.Collections.Immutable;
using Movies.Domain;
using Movies.Domain.ValueObjects;
using Shared.Domain.Criteria;
using Movies.Infraestructure.TheMovieDb.Entities;
using MovieDomain = Movies.Domain.Movie;
using System.Net.Http.Json;
using Movies.Infraestructure.TheMovieDb.Configuration;


namespace Movies.Infraestructure.TheMovieDb
{
    public class TheMovieDBRepository (MovieRespositoryConfiguration config) : MovieRepository
    {
      private readonly ConfigTheMovieDBRespository _config = (ConfigTheMovieDBRespository) config;

      public async Task<MovieDomain?> findById(MovieId movieId)
      {        
        TheMovieDBCriteriaTransformation criteriaTransformation = new TheMovieDBCriteriaTransformation();
        string apiURL = ConfigTheMovieDBRespository.MOVIE_FIND + movieId.value + criteriaTransformation.getEmptyCriteria() ; 

        HttpResponseMessage response = await _config.Client.GetAsync(apiURL);

        if (!response.IsSuccessStatusCode)
          return null;

        Entities.Movie? movie = await response.Content.ReadAsAsync<Entities.Movie>();

        if (null == movie)
          return null;
        
        ConfigMovie? confMoviesImages = await _config.GetConfigMovies();

        if (null == confMoviesImages)
          return null;

        return movie.toMovieDomain(confMoviesImages.images.base_url + confMoviesImages.images.backdrop_sizes[2]);
      }

      public async Task<MovieSearchResults> searchByCriteria(Criteria criteria)
      {
        TheMovieDBCriteriaTransformation criteriaTransformation = new TheMovieDBCriteriaTransformation(criteria);

        string queryType = criteriaTransformation.isSearch() ? ConfigTheMovieDBRespository.SEARCH : ConfigTheMovieDBRespository.DISCOVER;

        string apiURL = queryType + criteriaTransformation.getCriterias(); 

        HttpResponseMessage response = await _config.Client.GetAsync(apiURL);

        if (!response.IsSuccessStatusCode)
          return new MovieSearchResults(ImmutableList<MovieDomain>.Empty, criteriaTransformation.getPagination());
        
        MovieAPIResult? apiResults = await response.Content.ReadAsAsync<MovieAPIResult>();

        if (null == apiResults)
          return new MovieSearchResults([], criteria.pagination);
        
        ConfigMovie? confMoviesImages = await _config.GetConfigMovies();

        if (null == confMoviesImages)
          return new MovieSearchResults([], criteria.pagination);

        ImmutableList<MovieDomain> movies = (from movie in apiResults.results
          select movie.toMovieDomain(confMoviesImages.images.base_url + confMoviesImages.images.backdrop_sizes[2])).ToImmutableList();
        
        return new MovieSearchResults(movies, new Pagination(apiResults.page, apiResults.total_pages));

      }

    }
}