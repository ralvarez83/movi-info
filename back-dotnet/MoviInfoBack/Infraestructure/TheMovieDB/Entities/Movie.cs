using MovieDomain =  Domain.Movies;
using Infraestructure.TheMovieDb.Entities.Attributes;
using Domain.Movies.ValueObjects;

namespace Infraestructure.TheMovieDb.Entities
{
  public class Movie
  {
    public bool adult {get; set;}
    public string backdrop_path {get; set;} = string.Empty;
    public int budget {get; set;} 
    public Genre[] genres {get; set;}   = [];
    public string homepage {get; set;} = string.Empty;
    public int id {get; set;} 
    public string imdb_id {get; set;} = string.Empty;
    public string[] origin_country {get; set;}  = [];
    public string original_language {get; set;}  = string.Empty;
    public string original_title {get; set;} = string.Empty;
    public string overview {get; set;} = string.Empty;
    public float popularity {get; set;}
    public string poster_path {get; set;} = string.Empty;
    public ProductionCompany[] production_companies {get; set;}  = [];
    public ProductionCountry[] production_contries {get; set;}  = [];
    public DateTime release_date {get; set;}
    public float revenue {get; set;}
    public long runtime {get; set;}
    public SpokenLanguage[] spoken_languages {get; set;}  = [];
    public string status {get; set;} = string.Empty;
    public string tagline {get; set;} = string.Empty;
    public string title {get; set;} = string.Empty;
    public bool video {get; set;}
    public float vote_average {get; set;}
    public long vote_count {get; set;}

    public MovieDomain.Movie toMovieDomain(string base_url){
      
      return new MovieDomain.Movie (new MovieId(id.ToString()), title, overview, adult, new Uri(base_url + poster_path), new Uri(base_url + backdrop_path), new Uri(MovieDomain.Movie.IMDB_BASE_LINK + imdb_id), vote_average);

    }
    
  }
    
  
}