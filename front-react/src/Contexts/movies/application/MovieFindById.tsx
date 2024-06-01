import { Movie } from "../domain/Movie";
import { MovieRepository } from "../domain/MovieRepository";
import { MovieFind } from "./types";

export class MovieFindById implements MovieFind {
  constructor(private readonly repository: MovieRepository, private readonly movieId: String) {}

  /**
   * find
   */
  async find(): Promise<Movie|undefined> {
    return await this.repository.findById(this.movieId);
  }
}