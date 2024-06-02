import { Movie } from './Movie'
import { moviesState } from '../hooks/MoviesState'
import { TextFilter } from './shared/TextFilter'
import { MovieRepository } from '../../../Contexts/movies/domain/MovieRepository'
interface Props {
  repository: MovieRepository
}

export const MoviesList: React.FC<Props> = ({repository}) => {
  const {
    movieList,
    textFilter,
    setTextFilter
  } = moviesState(repository)

  return (
    <main className='movie-list'>
      <h2>Todas las películas</h2>
      <aside>
        <TextFilter filter={textFilter} placeholder='Busca por texto...' setFilter={setTextFilter} />
      </aside>
      <section>
        {movieList.map((movie) => (
            <Movie key={movie.id} image_path={movie.horizontalImagePath} {... movie} />
          ))}
      </section>
    </main>
  )
}
