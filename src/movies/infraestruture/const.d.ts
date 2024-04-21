export const BASE_URL = 'https://api.themoviedb.org/3/'
export const DISCOVER = 'discover/movie?include_adult=false&include_video=false&language=es-ES&sort_by=popularity.desc'
export const AUTHORIZATION = 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NjhiZDdjYjc4NDRkZWZkYzNjZTJhYjRhNzI4NTM0MSIsInN1YiI6IjY2MGQ1YmM1ZTAzOWYxMDE3Y2U3OTczOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.U04pBIxx2V6BBbL0ZaoJzWYSrv0PAIJWQHdNB48vuLs'
export const CONFIG = 'configuration'

export const MOVIE_API_CONFIG_GET = {
  method: 'GET',
  headers: {
    accept: 'application/json',
    Authorization: AUTHORIZATION
  }
}
