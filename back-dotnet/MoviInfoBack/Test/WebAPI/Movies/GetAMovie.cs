using Movies.Application.Dtos;
using Xunit.Gherkin.Quick;

namespace Test.WebAPI.Movies
{
  [FeatureFile("./WebAPI/Movies/GetAMovie.feature")]
  public sealed class GetAMovie : Feature
  {
    // La asigna el paso Given antes de que corra ningún Then, pero eso el compilador no puede
    // deducirlo de la ejecución que orquesta Gherkin, así que se marca con null! en vez de
    // declararla anulable y tener que comprobarla en cada paso.
    private HttpResponseMessage response {get; set;} = null!;
    
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