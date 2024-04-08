import { Grid } from '@mui/material'
import { Movie } from './Movie'
import { useMovies } from '../hooks/useMovies'

export const MoviesList = (): JSX.Element => {
  const {
    movies
  } = useMovies()

  return (
    <Grid sx={{ flexGrow: 1 }} container spacing={2}>
      {movies.map((movie) => (
          <Movie key={movie.id} movie={movie} />
        ))}
    </Grid>
  )
}
