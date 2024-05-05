import { type Movie as MovieDomain } from "../../../domain/Movie";
import { Movie } from "./Movie";
import { ConfigMovie } from "../types.d"

export class MovieTransformation {
  constructor(private readonly movie: Movie, private readonly config?: ConfigMovie) {}

  transformMovie (): MovieDomain | undefined{
    let movieDomain : MovieDomain | undefined = undefined

    if (this.movie === undefined) {
      return movieDomain 
    }

    if (this.config === undefined || this.config?.images.base_url === '')
      return {
        ... this.movie,
        vertical_image_path: '/video-icon.jpg',
        horizontal_image_path: '/video-icon.jpg',
      }

      const backdrop_path = this.config?.images.base_url + this.config?.images.backdrop_sizes[2] + this.movie.backdrop_path
      const poster_path = this.config?.images.base_url + this.config?.images.poster_sizes[2] + this.movie.poster_path
    
    
    movieDomain = {
      ... this.movie,
      vertical_image_path: poster_path,
      horizontal_image_path: backdrop_path,
    }

    return movieDomain
  }
}