using System.Security.Cryptography.X509Certificates;
using MovieDomine = Movies.Domain.Movie;

namespace Movies.Application.DTO
{
  public readonly record struct Movie (string id, string title, string overview, bool adult, string verticalImagePath, string horizontalImagePath, string imdbLink, float usersVote);
}