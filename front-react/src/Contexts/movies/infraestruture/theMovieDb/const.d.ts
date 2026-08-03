// ATENCIÓN: este adaptador ataca la API de TheMovieDB directamente desde el navegador y hoy no
// se usa (App.tsx trabaja contra DotNetBackRepository). Se conserva como referencia del patrón
// Repository con dos implementaciones.
//
// Cualquier token que se use aquí acaba dentro del bundle JavaScript y, por tanto, es público:
// las variables VITE_* se sustituyen en tiempo de build, NO son un secreto. Precisamente por eso
// existe el back en .NET, que es quien guarda el token de verdad. No pongas aquí un token bueno.
export const BASE_URL = 'https://api.themoviedb.org/3/'
export const DISCOVER = 'discover/movie?'
export const SEARCH = '/search/movie?'
export const AUTHORIZATION = `Bearer ${import.meta.env.VITE_THE_MOVIE_DB_TOKEN ?? ''}`
export const CONFIG = 'configuration'
export const MOVIE_FIND = 'movie/'
export const IMDB_BASE_LINK = 'https://www.imdb.com/title/'

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