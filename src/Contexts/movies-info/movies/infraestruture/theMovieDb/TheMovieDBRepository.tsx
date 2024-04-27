import { MovieList } from "../../domain/Movie.d"
import { MovieRepository } from "../../domain/MovieRepository"
import { Movie as MovieDB } from "./Movie.d"
import { BASE_URL, DISCOVER, MOVIE_API_CONFIG_GET } from "./const.d"
import { ConfigMovie } from "./types.d"
import { TheMovieDBCriteria } from "./TheMovieDBCriteria"
import { getConfig } from "./MoviesConfig"

export class TheMovieDBRepository implements MovieRepository{
  #config?: ConfigMovie = undefined

  async searchByCriteria(criteria: TheMovieDBCriteria): Promise<MovieList> {

    if (this.#config === undefined) 
      this.#config = await getConfig()

    const res = await fetch(BASE_URL + DISCOVER, MOVIE_API_CONFIG_GET)
    // console.log('res', res);
    if (!res.ok) {
      console.error('Error fetching movies')
      return []
    }

    const movies = await res.json()

    const newMovieList = movies.results.map((movie: MovieDB) => {
      if (this.#config === undefined || this.#config?.images.base_url === '')
        return {
          ... movie,
          vertical_image_path: '/video-icon.jpg',
          horizontal_image_path: '/video-icon.jpg',
        }

        const backdrop_path = this.#config?.images.base_url + this.#config?.images.backdrop_sizes[2] + movie.backdrop_path
        const poster_path = this.#config?.images.base_url + this.#config?.images.poster_sizes[2] + movie.poster_path
      
        return {
          ... movie,
          vertical_image_path: poster_path,
          horizontal_image_path: backdrop_path,
          
        }
    })
    //console.log("movies", newMovieList);
    return newMovieList
  }

}