namespace WebAPI.Configurations
{
  public class TheMovieDBOptions
  {
    public const string Name = "TheMovieDB";
    public string Authorization {get; set;} = String.Empty;
    public string AuthorizationType {get; set;} = String.Empty;
    public string BaseURL {get; set;} = String.Empty;
  }
}