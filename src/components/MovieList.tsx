import { Movie } from './Movie'
import { useMovies } from '../hooks/useMovies'

export const MoviesList = (): JSX.Element => {
  const {
    movies
  } = useMovies()

  return (
    <main className='movie-list'>
      <h2>Todas las películas</h2>
      <section>
        {movies.map((movie) => (
            <Movie key={movie.id} {... movie} />
          ))}
      </section>
    </main>
  )
}
