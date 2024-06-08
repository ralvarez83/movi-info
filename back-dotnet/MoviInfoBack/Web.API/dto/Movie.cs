using MovieDomine = Domain.Movies.Movie;

namespace WebAPI.dto
{
  public record Movie
  {
    public string id {get; set;}
    public string title {get; set;}
    public string overview {get; set;}
    public bool adult {get; set;}
    public string verticalImagePath {get; set;}
    public string horizontalImagePath {get; set;}
    public string imdbLink {get; set;}
    public float usersVote {get; set;}
    public static Movie TransformToMovieDTO (MovieDomine movieDomine){
      return new Movie {
        id = movieDomine.id.value,
        title = movieDomine.title,
        overview = movieDomine.overview,
        adult = movieDomine.adult,
        verticalImagePath = (null != movieDomine.verticalImagePath)? movieDomine.verticalImagePath.AbsoluteUri:string.Empty,
        horizontalImagePath = (null != movieDomine.horizontalImagePath)? movieDomine.horizontalImagePath.AbsoluteUri:string.Empty,
        imdbLink = (null != movieDomine.imdbLink)? movieDomine.imdbLink.AbsoluteUri:string.Empty,
        usersVote = movieDomine.usersVote
      };
    }
  }
}