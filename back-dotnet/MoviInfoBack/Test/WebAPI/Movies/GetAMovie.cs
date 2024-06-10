using Movies.Application.DTO;
using Xunit.Gherkin.Quick;

namespace Test.WebAPI.Movies
{
  [FeatureFile("./WebAPI/Movies/GetAMovie.feature")]
  public sealed class GetAMovie : Feature
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
    public void Then_Respons_Should_Be(int statusCode)
    {
       
      Assert.Equal<int>(statusCode, (int) this.response.StatusCode);
    }
    
    [And(@"the result should '(.*)'")]
    public async Task Then_The_Title_Should_Be_Equal(string movieTitle)
    {
      Movie movie = await this.response.Content.ReadAsAsync<Movie>();
      Assert.Equal(movieTitle, movie.title);
    }
  }
}