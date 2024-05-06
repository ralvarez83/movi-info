import { type Movie as MovieDomain } from "../../../domain/Movie";
import { Movie } from "./Movie";
import { ConfigMovie } from "../types.d"
import { IMDB_BASE_LINK } from "../const.d";

export class MovieTransformation {
  constructor(private readonly movie: Movie, private readonly config?: ConfigMovie) {}

  transformMovie (): MovieDomain | undefined{
    let movieDomain : MovieDomain | undefined = undefined

    if (this.movie === undefined) {
      return movieDomain 
    }
    const imdbLink = IMDB_BASE_LINK  + this.movie.imdb_id
    let backdrop_path = ''
    let poster_path = ''

    if (this.config === undefined || this.config?.images.base_url === ''){
      backdrop_path = '/video-icon.jpg'
      poster_path = '/video-icon.jpg'
    }
    else{
      backdrop_path = this.config?.images.base_url + this.config?.images.backdrop_sizes[2] + this.movie.backdrop_path
      poster_path = this.config?.images.base_url + this.config?.images.poster_sizes[2] + this.movie.poster_path
    }
    
    movieDomain = {
      id: this.movie.id.toString(),
      title: this.movie.title,
      overview: this.movie.overview,
      adult: this.movie.adult,
      verticalImagePath: poster_path,
      horizontalImagePath: backdrop_path,
      imdbLink,
      usersVote: this.movie.vote_average / 10
    }

    return movieDomain
  }
}