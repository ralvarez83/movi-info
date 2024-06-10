using MovieDomine = Movies.Domain.Movie;

namespace Movies.Application.DTO
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
  }
}