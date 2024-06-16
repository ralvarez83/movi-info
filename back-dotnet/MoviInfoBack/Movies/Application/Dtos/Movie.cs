namespace Movies.Application.Dtos
{
  public readonly record struct Movie (string id, string title, string overview, bool adult, string verticalImagePath, string horizontalImagePath, string imdbLink, float usersVote);
}