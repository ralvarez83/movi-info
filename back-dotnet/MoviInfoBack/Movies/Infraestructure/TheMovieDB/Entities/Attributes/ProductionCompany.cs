namespace Movies.Infraestructure.TheMovieDb.Entities.Attributes
{
  public sealed record ProductionCompany(
    int id,
    string logo_path,
    string name,
    string origin_country
  );
}