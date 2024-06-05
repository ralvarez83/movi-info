using MovieDomine = Domain.Movies.Movie;

namespace WebAPI.dto
{
  public record Movie
  {
    public string id {get; init;}
    public string title {get; init;}
    public string overview {get; init;}
    public bool adult {get; init;}
    public string verticalImagePath {get; init;}
    public string horizontalImagePath {get; init;}
    public string imdbLink {get; init;}
    public float usersVote {get; init;}
    public Movie(MovieDomine movieDomine){
      id = movieDomine.id.value;
      title = movieDomine.title;
      overview = movieDomine.overview;
      adult = movieDomine.adult;
      verticalImagePath = (null != movieDomine.verticalImagePath)? movieDomine.verticalImagePath.AbsoluteUri:string.Empty;
      horizontalImagePath = (null != movieDomine.horizontalImagePath)? movieDomine.horizontalImagePath.AbsoluteUri:string.Empty;
      imdbLink = (null != movieDomine.imdbLink)? movieDomine.imdbLink.AbsoluteUri:string.Empty;
      usersVote = movieDomine.usersVote;

    }
  }
}