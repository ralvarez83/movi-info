namespace Movies.Infraestructure.TheMovieDb.Entities.Attributes
{
  public sealed record ProductionCountry(
    string iso_3166_1,
    string name
  );
}