using Domain.Movies.ValueObjects;
using Domain.Shared.ValueObjects.Links;

namespace Domain.Movies
{
  public sealed class Movie
  {
    public const string IMDB_BASE_LINK = "https://www.imdb.com/title/";
    public MovieId id {get; init;}
    public string title {get; init;}
    public string overview {get; init;}
    public bool adult {get; init;}
    public LinkRegex? verticalImagePath {get; init;}
    public LinkRegex? horizontalImagePath {get; init;}
    public LinkRegex? imdbLink {get; init;}
    public float usersVote {get; init;}

    public Movie (MovieId id, string title, string overview, bool adult, LinkRegex verticalImagePath, LinkRegex horizontalImagePath, LinkRegex imdbLink, float usersVote){
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