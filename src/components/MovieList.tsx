import { ImageList } from '@mui/material'
import { Movie } from './Movie'
import { useMovies } from '../hooks/useMovies'

export const MoviesList = (): JSX.Element => {
  const {
    movies
  } = useMovies()

  return (
    <ImageList variant="quilted" rowHeight={164}>
      {movies.map((movie) => (
          <Movie key={movie.id} movie={movie} />
        ))}
    </ImageList>
  )
}
