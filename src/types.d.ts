export interface Movie {
  title: string
  id: string
  release_date: number
  author: string
  backdrop_path: string,
  poster_path: string
  overview: string
}

export type MovieId = Pick<Movie, 'id'>
export type MovieTitle = Pick<Movie, 'title'>

export type MovieList = Movie[]

export interface State {
  movies: MovieList
}
