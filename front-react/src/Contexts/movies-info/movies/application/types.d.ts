export interface MoviesSearch{
  search () : Promise<MovieSearchResults>
}

export interface MovieFind {
  find() : Promise<Movie>
}