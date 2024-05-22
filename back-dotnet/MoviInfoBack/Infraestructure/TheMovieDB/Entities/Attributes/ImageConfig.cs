namespace Infraestructure.TheMovieDb.Entities.Attributes
{
  public sealed class ImageConfig
  {
    public string base_url {get; set;} = "";
    public string[] backdrop_sizes {get; set;} = [];
    public string[] poster_sizes {get; set;} = [];
  }
}