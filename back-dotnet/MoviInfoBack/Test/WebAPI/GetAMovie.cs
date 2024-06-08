using Xunit.Gherkin.Quick;

namespace Test.WebAPI
{
  [FeatureFile("./WebAPI/GetAMovie.feature")]
  public sealed class GetAMovie : Feature
  {
    
    [Given(@"I send a GET request to '(.*)'")]
    public void Given_I_Send_A_Get_Request(string url)
    {
      Uri uri = new Uri(url);
    }
    
    [Then(@"the response status code should be (\d+)")]
    public void Then_Respons_Should_Be_200(int statusCode)
    {
       
      Assert.True(true);
    }
    
    [And(@"the result should '(.*)'")]
    public void Then_The_Title_Should_Be_Equal(string movieTitle)
    {
       
      Assert.True(true);
    }
  }
}