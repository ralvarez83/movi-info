import {Factory} from "fishery"
import { MovieList, MovieSearchResults } from "../../../../src/Contexts/movies/domain/Movie";
import {generateMovieRandonList} from "../domain/MovieFactory"
import {generatePaginationRandom} from "../domain/PaginationFactory"
import { Pagination } from "../../../../src/Contexts/Shared/Domain/Criteria/Pagination";


const movieSearchResultFactory = Factory.define<MovieSearchResults>(({}) => ({
  movies: generateMovieRandonList(20,20) as MovieList,
  pagination: generatePaginationRandom({}) as Pagination
}));

export function generateMovieSearchResultRandom (param){
  return movieSearchResultFactory.build(param);
}