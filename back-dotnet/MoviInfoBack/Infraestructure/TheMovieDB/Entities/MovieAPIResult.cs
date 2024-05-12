namespace Infraestructure.TheMovieDb.Entities
{
  public sealed record MovieAPIResult ( 
    int page,
    Movie[] results,
    int total_pages,
    int total_results);
}