import { useEffect, useReducer } from 'react'
import { type MovieList } from '../movies/domain/Movie'
import { getConfig } from '../movies/infraestruture/MoviesConfig'
import { getMovies } from '../movies/infraestruture/movies'
import { State } from '../types'

const initialState = {
  movies: []
}

export const ACTIONS = {
  INIT: 'INIT'
} as const

interface Action { type: typeof ACTIONS.INIT, payload: { movies: MovieList } }

const reducer = (state: State, action: Action): State => {
  if (action.type === ACTIONS.INIT) {
    const { movies } = action.payload
    return {
      ...state,
      movies
    }
  }
  return state
}

export const useMovies = (): {
  movies: MovieList
} => {
  const [{ movies }, dispatch] = useReducer(reducer, initialState)

  useEffect(() => {
    getConfig()
      .then(config => {
        getMovies(config)
        .then(movies => {
          dispatch({ type: ACTIONS.INIT, payload: { movies } })
        })
        .catch(err => { console.error(err) })
      }
      )
      .catch(err => {console.error(err)})
  }, [])

  return {
    movies
  }
}
