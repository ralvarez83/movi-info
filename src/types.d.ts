import { Filters } from "./Contexts/Shared/Domain/Criteria/Filters/Filters";
import { MovieList } from "./Contexts/movies-info/movies/domain/Movie";
export interface State {
  movies: MovieList,
  textFilter: Filter
}
