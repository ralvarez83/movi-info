using System.Data;
using System.Net.Http.Headers;
using Infraestructure.TheMovieDb.Entities.Attributes;

namespace Infraestructure.TheMovieDb.Entities
{
  public sealed record ConfigMovie{
    public const string BASE_URL = "https://api.themoviedb.org/3/";
    public const string DISCOVER = "discover/movie";
    public const string SEARCH = "/search/movie";
    private const string CONFIG = "configuration";
    public const string MOVIE_FIND = "movie/";

    public string Authorization {get; set;} = "";
    public HttpClient Client {get; private set;} = new HttpClient();

    public ImageConfig images {get; set;}
    
    public async static Task<ConfigMovie?> GetConfig (string authorization, Uri baseURL, string authorizationType){
       HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(ConfigMovie.BASE_URL)
        };

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationType, authorization);

        string apiURL = ConfigMovie.CONFIG; 

        HttpResponseMessage response = await client.GetAsync(apiURL);

        if (!response.IsSuccessStatusCode)
          return null;

        ConfigMovie configMovie = await response.Content.ReadAsAsync<ConfigMovie>();
        
        configMovie.Client = client;

        return configMovie;

    }
    
  }
}