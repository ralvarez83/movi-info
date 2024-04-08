export interface Movie {
  title: string
  id: string
  age: number
  author: string
}

export type MovieId = Pick<Movie, 'id'>
export type MovieTitle = Pick<Movie, 'title'>

export type MovieList = Movie[]

export interface State {
  movies: MovieList
}
