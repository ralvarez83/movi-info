import { useEffect, useState } from 'react'
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

export function moviesState(repository: MovieRepository): {
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

  const getMovies = () => {
    if (!pagination.isLastPage()){
      setIsLoading(true);
      const order: Order = new Order("", OrderType.NONE)
      const filters: Filters = new Filters()
      filters.add(textFilter)
      //console.log("Current page: ", pagination.page, " of ", pagination.totalPage)
      const criteria: Criteria = new Criteria(filters, order, pagination.getNextPage())

      //console.log("Next page: ", criteria.pagination.page)
      const movieSearcher = new MoviesSearchByCriteria(repository, criteria)
      movieSearcher.search().then (moviesFound => {
        //console.log("movieFound: ", moviesFound)
        setMoviesList([
          ... movieList,
          ... moviesFound.movies
        ])
        setPagination(moviesFound.pagination)
        setIsLoading(false);
      }
      );
    }
  }

  return {
    movieList,
    textFilter,
    pagination,
    isLoading,
    setTextFilter,
    getMovies
  }
}
