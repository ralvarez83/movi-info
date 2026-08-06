import { useCallback, useEffect, useRef, useState } from 'react'
import { type MovieList} from '../../../Contexts/movies/domain/Movie'
// import { TheMovieDBRepository } from '../../../Contexts/movies-info/movies/infraestruture/theMovieDb/TheMovieDBRepository'
import { Filter, FilterOperator } from '../../../Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d'
import { Order } from '../../../Contexts/Shared/Domain/Criteria/Order/Order'
import { OrderType } from '../../../Contexts/Shared/Domain/Criteria/Order/OrderTypes.d'
import { Pagination } from '../../../Contexts/Shared/Domain/Criteria/Pagination'
import { Filters } from '../../../Contexts/Shared/Domain/Criteria/Filters/Filters'
import { Criteria } from '../../../Contexts/Shared/Domain/Criteria/Criteria'
import { MoviesSearchByCriteria } from '../../../Contexts/movies/application/MoviesSearchByCriteria'
import { MovieRepository } from '../../../Contexts/movies/domain/MovieRepository'

export function useMoviesState(repository: MovieRepository): {
  movieList: MovieList
  textFilter: Filter,
  pagination: Pagination,
  isLoading: boolean,
  setTextFilter: React.Dispatch<React.SetStateAction<Filter>>,
  getMovies: () => void
} {

  const initialMovies : MovieList = []
  const [movieList, setMoviesList] = useState(initialMovies);

  const initialFilter: Filter = {
    field: 'byText',
    operator: FilterOperator.CONTAINS,
    value: ''
  }
  const [textFilter, setTextFilter] = useState(initialFilter);
  const [pagination, setPagination] = useState(new Pagination(0));
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    
    setPagination(new Pagination(0))
    setMoviesList([])

  }, [textFilter]);

  // Memorizada para que su identidad solo cambie cuando cambia algo de lo que depende. De ella
  // cuelga el efecto de InfinitePagination, que sin esto recreaba el IntersectionObserver en
  // cada render.
  // Evita que se pida dos veces la misma página. El observador de InfinitePagination puede
  // dispararse otra vez antes de que llegue la respuesta anterior, y entonces las dos peticiones
  // parten de la misma paginación y acaban añadiendo las mismas películas. Va en una referencia
  // y no en estado porque hay que consultarlo en el momento, sin esperar a un nuevo render.
  const isFetching = useRef(false)

  const getMovies = useCallback(() => {
    if (!pagination.isLastPage() && !isFetching.current){
      isFetching.current = true
      setIsLoading(true);
      const order: Order = new Order("", OrderType.NONE)
      const filters: Filters = new Filters()
      filters.add(textFilter)
      const criteria: Criteria = new Criteria(filters, order, pagination.getNextPage())

      const movieSearcher = new MoviesSearchByCriteria(repository, criteria)
      movieSearcher.search().then (moviesFound => {
        // Actualización funcional: además de quitar movieList de las dependencias, evita perder
        // una página si llegaran dos respuestas antes de volver a renderizar.
        setMoviesList(previousMovies => [
          ... previousMovies,
          ... moviesFound.movies
        ])
        setPagination(moviesFound.pagination)
        setIsLoading(false);
        isFetching.current = false
      }
      );
    }
  }, [repository, textFilter, pagination])

  return {
    movieList,
    textFilter,
    pagination,
    isLoading,
    setTextFilter,
    getMovies
  }
}
