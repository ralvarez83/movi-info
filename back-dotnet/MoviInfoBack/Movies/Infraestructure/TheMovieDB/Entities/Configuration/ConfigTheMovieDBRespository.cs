using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Movies.Domain;
using Movies.Infraestructure.TheMovieDb.Entities.Attributes;

namespace Movies.Infraestructure.TheMovieDb.Configuration
{
  public sealed class ConfigTheMovieDBRespository : MovieRespositoryConfiguration{

    private TheMovieDBOptions _theMovieDBOptions {get; set;}
    public const string BASE_URL = "https://api.themoviedb.org/3/";
    public const string DISCOVER = "discover/movie";
    public const string SEARCH = "search/movie";
    private const string CONFIG = "configuration";
    public const string MOVIE_FIND = "movie/";
    public HttpClient Client {get; init;}

    private ConfigMovie? _configMovie {get; set;}

    public ConfigTheMovieDBRespository (IOptions<TheMovieDBOptions> options){

      _theMovieDBOptions = options.Value;

      Client = new HttpClient
      {
          BaseAddress = new Uri(_theMovieDBOptions.BaseURL)
      };

      Client.DefaultRequestHeaders.Accept.Clear();
      Client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
      Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_theMovieDBOptions.AuthorisationType, _theMovieDBOptions.Authorisation);      
    }
    
    public async Task<ConfigMovie?> GetConfigMovies (){
      if (null != _configMovie)
        return _configMovie;

      string apiURL = ConfigTheMovieDBRespository.CONFIG; 

      HttpResponseMessage response = await Client.GetAsync(apiURL);

      if (!response.IsSuccessStatusCode)
        return null;

      _configMovie = await response.Content.ReadFromJsonAsync<ConfigMovie>();

      return _configMovie;
    }
    
  }
}