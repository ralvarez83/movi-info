import { MovieList } from "../../domain/Movie.d"
import { MovieRepository } from "../../domain/MovieRepository"
import { Movie as MovieDB } from "./Entities/Movie"
import { BASE_URL, DISCOVER, MOVIE_API_CONFIG_GET, SEARCH } from "./const.d"
import { ConfigMovie } from "./types.d"
import { TheMovieDBCriteriaTransformation } from "./TheMovieDBCriteriaTransformation"
import { getConfig } from "./MoviesConfig"
import { Criteria } from "../../../../Shared/Domain/Criteria/Criteria"
import { Pagination } from "../../../../Shared/Domain/Criteria/Pagination"

export class TheMovieDBRepository implements MovieRepository{
  #config?: ConfigMovie = undefined

  async searchByCriteria(criteria: Criteria): Promise<MovieList> {

    const criteriaTransformation = new TheMovieDBCriteriaTransformation(criteria)

    if (this.#config === undefined) 
      this.#config = await getConfig()

    const queryType: string = (criteriaTransformation.isSearch())? SEARCH: DISCOVER

    console.log('QueryType: ', queryType)

    const res = await fetch(BASE_URL + queryType + criteriaTransformation.getCriterias(), MOVIE_API_CONFIG_GET)
    // console.log('res', res);
    if (!res.ok) {
      console.error('Error fetching movies')
      return {
        movies: [],
        pagination: criteriaTransformation.getPagination()
      }
    }

    const newMovieList = await res.json()

    const movies = newMovieList.results.map((movie: MovieDB) => {
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
    return {
      movies,
      pagination: new Pagination(newMovieList.page, newMovieList.total_pages) 
    }
  }

}