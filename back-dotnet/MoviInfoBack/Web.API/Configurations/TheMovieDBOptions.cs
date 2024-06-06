namespace WebAPI.Configurations
{
  public class TheMovieDBOptions
  {
    public const string Name = "TheMovieDB";
    public string Authorisation {get; set;} = String.Empty;
    public string AuthorisationType {get; set;} = String.Empty;
    public string BaseURL {get; set;} = String.Empty;
  }
}