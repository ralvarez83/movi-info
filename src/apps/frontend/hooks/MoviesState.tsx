import { useEffect, useReducer } from 'react'
import { type MovieList } from '../../../Contexts/movies-info/movies/domain/Movie'
import { State } from '../../../types'
import { TheMovieDBRepository } from '../../../Contexts/movies-info/movies/infraestruture/theMovieDb/TheMovieDBRepository'
import { MoviesSearchAll } from '../../../Contexts/movies-info/movies/application/MoviesSearchAll'
import { Filter } from '../../../Contexts/Shared/Domain/Criteria/Filters/Filter'
import { FilterOperator } from '../../../Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d'

const initialState = {
  movies: [],
  textFilter: new Filter("ByText", FilterOperator.CONTAINS, '')
}

export const ACTIONS = {
  INIT: 'INIT'
} as const

interface Action { type: typeof ACTIONS.INIT, payload: { movies: MovieList, textFilter: Filter } }

const reducer = (state: State, action: Action): State => {
  if (action.type === ACTIONS.INIT) {
    const { movies, textFilter } = action.payload
    return {
      ...state,
      movies,
      textFilter
    }
  }
  return state
}

export function moviesState(): {
  movies: MovieList
  textFilter: Filter
} {
  const [{ movies, textFilter }, dispatch] = useReducer(reducer, initialState)

  useEffect(() => {
    console.log("filtra")
    const movieSearcherRepository = new TheMovieDBRepository()
    const movieSearcher = new MoviesSearchAll(movieSearcherRepository)
    movieSearcher.search()
      .then(movies => {
        dispatch({ type: ACTIONS.INIT, payload: { movies, textFilter } })
      })
      .catch(err => { console.error(err) })
      .catch(err => { console.error(err) })
  }, [textFilter])

  return {
    movies,
    textFilter
  }
}
