import { Pagination } from "../../../Shared/Domain/Criteria/Pagination";

export type Movie = {
  adult:                 boolean;
  verticalImagePath:   string;
  horizontalImagePath: string;
  id:                    string;
  overview:              string;
  title:                 string;
  imdbLink:              string;
  usersVote:             number;
 }


export type MovieList =  Movie[]

export type MovieSearchResults = {
  movies: MovieList,
  pagination: Pagination
}