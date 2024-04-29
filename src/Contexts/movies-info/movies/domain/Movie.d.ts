import { Pagination } from "../../../Shared/Domain/Criteria/Pagination";

export type Movie = {
  adult:                 boolean;
  vertical_image_path:   string;
  horizontal_image_path: string;
  id:                    number;
  overview:              string;
  title:                 string;
 }


export type MovieList =  Movie[]

export type MovieSearchResults = {
  movies: MovieList,
  pagination: Pagination
}