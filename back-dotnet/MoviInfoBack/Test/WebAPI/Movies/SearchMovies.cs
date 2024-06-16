using Movies.Application.Dtos;
using Xunit.Gherkin.Quick;

namespace Test.WebAPI.Movies
{
  [FeatureFile("./WebAPI/Movies/SearchMovies.feature")]
  public sealed class SearchMovies : Feature
  {
    private HttpResponseMessage response {get; set;}
    
    [Given(@"I send a GET request to '(.*)'")]
    public async Task Given_I_Send_A_Get_Request(string url)
    {
      HttpClient client = new HttpClient
      {
          BaseAddress = new Uri("http://localhost:5021")
      };
      client.DefaultRequestHeaders.Accept.Clear();
      
      this.response = await client.GetAsync(url);
    }
    
    [Then(@"the response status code should be (\d+)")]
    public void Then_Respons_Should_Be_(int statusCode)
    {
       
      Assert.Equal<int>(statusCode, (int) this.response.StatusCode);
    }
    
    [And(@"the result should return (\d+) movies")]
    public async Task Then_Should_Return_N_Movies(int numberMovies)
    {
      MovieSearchResults movieResult = await this.response.Content.ReadAsAsync<MovieSearchResults>();
      Assert.Equal(numberMovies, movieResult.movies.Count);
    }

    [And(@"all result should have the word '(.*)' or '(.*)' in title or overview")]
    public async Task Then_Shuld_Return_Movies_With_Words(string spanishWord, string englishWord)
    {
      MovieSearchResults movieResult = await this.response.Content.ReadAsAsync<MovieSearchResults>();
      
      movieResult.movies.ForEach(movie => {
        Assert.True(movie.HasValue);
        Assert.True(
          movie.Value.title.ToLower().Contains(spanishWord.ToLower()) ||
          movie.Value.title.ToLower().Contains(englishWord.ToLower()) ||
          movie.Value.overview.ToLower().Contains(spanishWord.ToLower()) ||
          movie.Value.overview.ToLower().Contains(englishWord.ToLower())
        );
      });
    }
  }
}