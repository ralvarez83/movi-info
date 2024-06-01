import { Criteria } from "../../Shared/Domain/Criteria/Criteria";
import { Movie, MovieSearchResults } from "./Movie";

export interface MovieRepository {

  searchByCriteria(criteria: Criteria): Promise<MovieSearchResults>
  findById(movieId: String): Promise<Movie|undefined>
  
}