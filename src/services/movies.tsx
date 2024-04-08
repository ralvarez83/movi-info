import { Movie, type MovieList } from '../types'

const BASE_URL = 'https://api.themoviedb.org/3/'
const DISCOVER = 'discover/movie?include_adult=false&include_video=false&language=es-ES&sort_by=popularity.desc'
const AUTHORIZATION = 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs'
const CONFIG = 'configuration'

const options = {
  method: 'GET',
  headers: {
    accept: 'application/json',
    Authorization: AUTHORIZATION
  }
}

export interface ConfigMovie{
  images: {
    base_url: string
    backdrop_sizes: string[]
    poster_sizes: string[]
  }
}

export const fetchMovies = async (): Promise<MovieList> => {
  const res = await fetch(BASE_URL + DISCOVER, options)
  // console.log('res', res);
  if (!res.ok) {
    console.error('Error fetching movies')
    return []
  }

  const movies = await res.json()

  const config = await fetchConfig()

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

export const fetchConfig = async (): Promise<ConfigMovie> =>{
  const res = await fetch(BASE_URL + CONFIG, options)
  // console.log('res', res);
  if (!res.ok) {
    console.error('Error fetching movies')
    return {
      images: {
        base_url: '',
        backdrop_sizes: [],
        poster_sizes: []
      }
    }
  }

  const config = await res.json()
  // console.log("config", config);
  return config
}
