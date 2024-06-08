using Movies.Domain.ValueObjects;

namespace Movies.Domain
{
  public sealed class Movie
  {
    public const string IMDB_BASE_LINK = "https://www.imdb.com/title/";
    public MovieId id {get; init;}
    public string title {get; init;}
    public string overview {get; init;}
    public bool adult {get; init;}
    public Uri? verticalImagePath {get; init;}
    public Uri? horizontalImagePath {get; init;}
    public Uri? imdbLink {get; init;}
    public float usersVote {get; init;}

    public Movie (MovieId id, string title, string overview, bool adult, Uri verticalImagePath, Uri horizontalImagePath, Uri imdbLink, float usersVote){
      this.id = id;
      this.title = title;
      this.overview = overview;
      this.adult = adult;
      this.verticalImagePath = verticalImagePath;
      this.horizontalImagePath = horizontalImagePath;
      this.imdbLink = imdbLink;
      this.usersVote = usersVote;
    }

  }
}