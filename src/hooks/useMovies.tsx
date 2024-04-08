import { useEffect, useReducer } from 'react'
import { type State, type MovieList } from '../types'
import { fetchMovies } from '../services/movies'

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
    fetchMovies()
      .then(movies => {
        dispatch({ type: ACTIONS.INIT, payload: { movies } })
      })
      .catch(err => { console.error(err) })
  }, [])

  return {
    movies
  }
}
