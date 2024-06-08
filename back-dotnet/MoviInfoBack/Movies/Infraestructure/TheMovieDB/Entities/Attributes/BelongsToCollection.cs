namespace Movies.Infraestructure.TheMovieDb.Entities.Attributes
{
  public sealed record BelongsToCollection (string backdrop_path, int id, string name, string poster_path);
}