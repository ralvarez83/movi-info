export const BASE_URL = 'https://api.themoviedb.org/3/'
export const DISCOVER = 'discover/movie?'
export const SEARCH = '/search/movie?'
export const AUTHORIZATION = 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs'
export const CONFIG = 'configuration'
export const MOVIE_FIND = 'movie/'

export const MOVIE_API_CONFIG_GET = {
  method: 'GET',
  headers: {
    accept: 'application/json',
    Authorization: AUTHORIZATION
  }
}

export const FIXED_FILTER_ADULT = 'include_adult=false'
export const FIXED_FILTER_LANGUAJE = 'language=es-ES'
export const FILTER_CONCATENATION = '&'
export const FILTER_STARTED = '?'