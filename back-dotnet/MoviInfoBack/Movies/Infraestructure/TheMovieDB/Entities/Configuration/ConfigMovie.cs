using Movies.Infraestructure.TheMovieDb.Entities.Attributes;

namespace Movies.Infraestructure.TheMovieDb.Configuration;

public sealed class ConfigMovie
{

    public ImageConfig images {get; set;} = new ImageConfig();
  
}