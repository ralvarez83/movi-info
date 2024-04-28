import { Movie } from './Movie'
import { moviesState } from '../hooks/MoviesState'
import { TextFilter } from './shared/TextFilter'

export const MoviesList = (): JSX.Element => {
  const {
    movies,
    textFilter,
    setTextFilter
  } = moviesState()

  return (
    <main className='movie-list'>
      <h2>Todas las películas</h2>
      <aside>
        <TextFilter filter={textFilter} placeholder='Busca por texto...' setFilter={setTextFilter} />
      </aside>
      <section>
        {movies.map((movie) => (
            <Movie key={movie.id} image_path={movie.horizontal_image_path} {... movie} />
          ))}
      </section>
    </main>
  )
}
