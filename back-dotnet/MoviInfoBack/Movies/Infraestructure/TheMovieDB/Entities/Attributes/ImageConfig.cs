namespace Movies.Infraestructure.TheMovieDb.Entities.Attributes
{
  public sealed class ImageConfig
  {
    public string base_url {get; set;} = "";
    // TheMovieDB expone la misma CDN por HTTP y por HTTPS. Hay que servir siempre la versión
    // segura: si el front va por HTTPS, el navegador bloquea las imágenes HTTP por mixed content.
    public string secure_base_url {get; set;} = "";
    public string[] backdrop_sizes {get; set;} = [];
    public string[] poster_sizes {get; set;} = [];
  }
}