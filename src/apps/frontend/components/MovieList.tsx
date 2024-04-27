import { Movie } from './Movie'
import { moviesState } from '../hooks/MoviesState'
import { TextFilter } from './shared/TextFilter'
import { Filter } from '../../../Contexts/Shared/Domain/Criteria/Filters/Filter'
import { FilterOperator } from '../../../Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d'

export const MoviesList = (): JSX.Element => {
  const {
    movies,
    textFilter
  } = moviesState()

  const filterField : string = 'byText'

  const setFilterValue = (value:string): void => {
    if (value.length >= 3){
      textFilter.setValue(value)
    }
  }

  return (
    <main className='movie-list'>
      <h2>Todas las películas</h2>
      <aside>
        <TextFilter name={filterField} placeholder='Busca por texto...' setFilterValue={setFilterValue} />
      </aside>
      <section>
        {movies.map((movie) => (
            <Movie key={movie.id} image_path={movie.horizontal_image_path} {... movie} />
          ))}
      </section>
    </main>
  )
}
