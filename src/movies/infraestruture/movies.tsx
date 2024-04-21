import { type Movie, type MovieList } from '../domain/Movie'
import { BASE_URL, DISCOVER, MOVIE_API_CONFIG_GET } from './const.d'
import { ConfigMovie } from './types.d'


export const getMovies = async (config: ConfigMovie): Promise<MovieList> => {
  const res = await fetch(BASE_URL + DISCOVER, MOVIE_API_CONFIG_GET)
  // console.log('res', res);
  if (!res.ok) {
    console.error('Error fetching movies')
    return []
  }

  const movies = await res.json()

  const newMovieList = movies.results.map((movie: Movie) => {
    if (config.images.base_url === '')
      return {
        ... movie,
        backdrop_path: '/video-icon.jpg',
        poster_path: '/video-icon.jpg',
      }

      const backdrop_path = config.images.base_url + config.images.backdrop_sizes[2] + movie.backdrop_path
      const poster_path = config.images.base_url + config.images.poster_sizes[2] + movie.poster_path
    
      return {
        ... movie,
        backdrop_path,
        poster_path
      }
  })
  console.log("movies", newMovieList);
  return newMovieList
}

