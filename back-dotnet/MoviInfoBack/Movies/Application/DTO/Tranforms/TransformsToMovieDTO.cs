using MovieDomain = Movies.Domain.Movie;

namespace Movies.Application.DTO.Transforms
{
  public class TransformsToMovieDTO
  {
    public static Movie? Run (MovieDomain? movieToTranform){
      if (null == movieToTranform)
          return null;

      return new Movie {
        id = movieToTranform.id.value,
        title = movieToTranform.title,
        overview = movieToTranform.overview,
        adult = movieToTranform.adult,
        verticalImagePath = (null != movieToTranform.verticalImagePath)? movieToTranform.verticalImagePath.AbsoluteUri:string.Empty,
        horizontalImagePath = (null != movieToTranform.horizontalImagePath)? movieToTranform.horizontalImagePath.AbsoluteUri:string.Empty,
        imdbLink = (null != movieToTranform.imdbLink)? movieToTranform.imdbLink.AbsoluteUri:string.Empty,
        usersVote = movieToTranform.usersVote
      };
    }
    
  } 
}