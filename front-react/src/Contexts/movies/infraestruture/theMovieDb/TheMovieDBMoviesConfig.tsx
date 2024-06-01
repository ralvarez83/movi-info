import { BASE_URL, CONFIG, MOVIE_API_CONFIG_GET } from "./const.d"
import { ConfigMovie } from "./types.d"

export const getTheMovieDBConfig = async (): Promise<ConfigMovie> =>{
  const res = await fetch(BASE_URL + CONFIG, MOVIE_API_CONFIG_GET)
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

  return config
}