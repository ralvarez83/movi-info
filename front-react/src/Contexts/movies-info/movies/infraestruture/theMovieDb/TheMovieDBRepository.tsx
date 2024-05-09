import { Movie, MovieSearchResults } from "../../domain/Movie.d"
import { MovieRepository } from "../../domain/MovieRepository"
import { Movie as MovieDB } from "./Entities/Movie"
import { BASE_URL, DISCOVER, FILTER_STARTED, FIXED_FILTER_LANGUAJE, MOVIE_API_CONFIG_GET, MOVIE_FIND, SEARCH } from "./const.d"
import { ConfigMovie } from "./types.d"
import { TheMovieDBCriteriaTransformation } from "./TheMovieDBCriteriaTransformation"
import { getTheMovieDBConfig } from "./TheMovieDBMoviesConfig"
import { Criteria } from "../../../../Shared/Domain/Criteria/Criteria"
import { Pagination } from "../../../../Shared/Domain/Criteria/Pagination"
import { MovieTransformation } from "./Entities/MovieTransformation"

export class TheMovieDBRepository implements MovieRepository{
  #config?: ConfigMovie = undefined

  async searchByCriteria(criteria: Criteria): Promise<MovieSearchResults> {

    const criteriaTransformation = new TheMovieDBCriteriaTransformation(criteria)

    if (this.#config === undefined) 
      this.#config = await getTheMovieDBConfig()

    const queryType: string = (criteriaTransformation.isSearch())? SEARCH: DISCOVER

    const res = await fetch(BASE_URL + queryType + criteriaTransformation.getCriterias(), MOVIE_API_CONFIG_GET)
    // console.log('res', res);
    if (!res.ok) {
      return {
        movies: [],
        pagination: criteriaTransformation.getPagination()
      }
    }

    const newMovieList = await res.json()

    const movies = newMovieList.results.map((movie: MovieDB) => {
        const movieTransformater = new MovieTransformation(movie, this.#config)

        return movieTransformater.transformMovie();
    })
    //console.log("movies", newMovieList);
    return {
      movies,
      pagination: new Pagination(newMovieList.page, newMovieList.total_pages) 
    }
  }

  async findById (movieID: string): Promise<Movie|undefined> {
    
    if (this.#config === undefined) 
      this.#config = await getTheMovieDBConfig()

    const res = await fetch(BASE_URL + MOVIE_FIND + movieID + FILTER_STARTED + FIXED_FILTER_LANGUAJE, MOVIE_API_CONFIG_GET)

    if (!res.ok) {
      return undefined
    }
    
    const movieReturn = await res.json();

    const movieTransformater = new MovieTransformation(movieReturn, this.#config)

    return movieTransformater.transformMovie();

  }

}