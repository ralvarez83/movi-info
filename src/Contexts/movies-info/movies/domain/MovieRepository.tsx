import { Criteria } from "../../../Shared/Domain/Criteria/Criteria";
import { MovieList } from "./Movie";

export interface MovieRepository {

  searchByCriteria(criteria: Criteria): Promise<MovieList>
  
}