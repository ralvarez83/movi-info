import { Movie, MovieSearchResults } from "../../domain/Movie"
import { MovieRepository } from "../../domain/MovieRepository"
import { DotNetBackCriteriaTransformation } from "./DotNetBackCriteriaTransformation"
import { Criteria } from "../../../../Shared/Domain/Criteria/Criteria"

const SEARCH_END_POINT: string = "Search?"
const FIND_END_POINT: string = "Find"

export class DotNetBackRepository implements MovieRepository{
  
  #endPointURLAccess : string

  public constructor(endPointURLAccess: string){
    this.#endPointURLAccess = endPointURLAccess
  }

  async searchByCriteria(criteria: Criteria): Promise<MovieSearchResults> {

    const criteriaTransformation = new DotNetBackCriteriaTransformation(criteria)
    
    //console.log("Llamada a: ",this.#endPointURLAccess + SEARCH_END_POINT + criteriaTransformation.getCriterias())
    const res = await fetch(this.#endPointURLAccess + SEARCH_END_POINT + criteriaTransformation.getCriterias())
    console.log('res', res);
    if (!res.ok) {
      console.log("Error llamada: ", res.statusText)
      return {
        movies: [],
        pagination: criteriaTransformation.getPagination()
      }
    }

    const newMovieList = await res.json()

    //console.log("movies", newMovieList);
    return newMovieList
  }

  async findById (movieID: string): Promise<Movie|undefined> {
    
   
    const res = await fetch(this.#endPointURLAccess + FIND_END_POINT + movieID)

    if (!res.ok) {
      return undefined
    }
    
    const movieReturn = await res.json();

    return movieReturn;

  }

}