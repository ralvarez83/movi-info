namespace Movies.Infraestructure.TheMovieDb.Entities.Attributes
{
  public sealed record SpokenLanguage(
    string english_name,
    string iso_639_1,
    string name
  );
}