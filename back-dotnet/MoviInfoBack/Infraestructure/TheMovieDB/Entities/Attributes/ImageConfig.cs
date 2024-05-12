namespace Infraestructure.TheMovieDb.Entities.Attributes
{
  public sealed record ImageConfig(
    string base_url,
    string[] backdrop_siezes,
    string[] poster_sizes
  );
}