import { Criteria } from "../../../Shared/Domain/Criteria/Criteria";
import { MovieSearchResults } from "./Movie";

export interface MovieRepository {

  searchByCriteria(criteria: Criteria): Promise<MovieSearchResults>
  
}