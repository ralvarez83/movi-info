import { useEffect, useState } from 'react'
import { type MovieList} from '../../../Contexts/movies-info/movies/domain/Movie'
import { TheMovieDBRepository } from '../../../Contexts/movies-info/movies/infraestruture/theMovieDb/TheMovieDBRepository'
import { Filter, FilterOperator } from '../../../Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d'
import { Order } from '../../../Contexts/Shared/Domain/Criteria/Order/Order'
import { OrderType } from '../../../Contexts/Shared/Domain/Criteria/Order/OrderTypes.d'
import { Pagination } from '../../../Contexts/Shared/Domain/Criteria/Pagination'
import { Filters } from '../../../Contexts/Shared/Domain/Criteria/Filters/Filters'
import { Criteria } from '../../../Contexts/Shared/Domain/Criteria/Criteria'
import { MoviesSearchByCriteria } from '../../../Contexts/movies-info/movies/application/MoviesSearchByCriteria'


export function moviesState(): {
  movieList: MovieList
  textFilter: Filter,
  pagination: Pagination,
  isLoading: boolean,
  setTextFilter: React.Dispatch<React.SetStateAction<Filter>>,
  setPagination: React.Dispatch<React.SetStateAction<Pagination>>
} {
  const initialMovies : MovieList = {
    movies: [],
    pagination: new Pagination(0)
  }
  const [movieList, setMoviesList] = useState(initialMovies);
  const initialFilter: Filter = {
    field: 'byText',
    operator: FilterOperator.CONTAINS,
    value: ''
  }
  const [textFilter, setTextFilter] = useState(initialFilter);
  const [pagination, setPagination] = useState(initialMovies.pagination);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {

    if (pagination.page !== 0){
      setIsLoading(true);
      const order: Order = new Order("", OrderType.NONE)
      const filters: Filters = new Filters()
      filters.add(textFilter)
  
      const criteria: Criteria = new Criteria(filters, order, pagination)
      
      const movieSearcherRepository = new TheMovieDBRepository()
  
      const movieSearcher = new MoviesSearchByCriteria(movieSearcherRepository, criteria)
      //const movieSearcher = new MoviesSearchAll(movieSearcherRepository)
      movieSearcher.search()
        .then(newMovieList => {
          let concatMovieList : MovieList = {
            movies:  movieList.movies.concat(newMovieList.movies),
            pagination: newMovieList.pagination
          }
          // setMoviesList(concatMovieList)
          setMoviesList(newMovieList)

					console.log("Page return: ", newMovieList.pagination.page)
        })
        .catch(err => { console.error(err) })
        .catch(err => { console.error(err) })
  
      setIsLoading(false);
    }
  }, [textFilter, pagination])

  return {
    movieList,
    textFilter,
    pagination,
    isLoading,
    setTextFilter,
    setPagination
  }
}
