import { Criteria } from "../../Shared/Domain/Criteria/Criteria";
import { MovieSearchResults } from "../domain/Movie";
import { MovieRepository } from "../domain/MovieRepository";
import { MoviesSearch } from "./types";

export class MoviesSearchByCriteria implements MoviesSearch {
  constructor(private readonly repository: MovieRepository, private readonly criteria: Criteria) {}

  async search () : Promise<MovieSearchResults> {
    return await this.repository.searchByCriteria(this.criteria);
  }

}