import { type MovieList } from '../types'

const BASE_URL = 'https://api.themoviedb.org/3/'
const DISCOVER = 'discover/movie?include_adult=false&include_video=false&language=es-ES&sort_by=popularity.desc'
const AUTHORIZATION = 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs'

const options = {
  method: 'GET',
  headers: {
    accept: 'application/json',
    Authorization: AUTHORIZATION
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
  return movies.results
}
