import { useEffect, useRef, useState } from 'react'
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
  // pagination: Pagination,
  isLoading: boolean,
  observerTargetEndPage: React.MutableRefObject<null>,
  setTextFilter: React.Dispatch<React.SetStateAction<Filter>>,
  // setPagination: React.Dispatch<React.SetStateAction<Pagination>>
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
  // const [pagination, setPagination] = useState(initialMovies.pagination);
  const [isLoading, setIsLoading] = useState(false);
	const observerTargetEndPage = useRef(null);

  useEffect(() => {
    const observer = new IntersectionObserver(
      entries => {
        if (entries[0].isIntersecting) {
          if(!movieList.pagination.isLastPage()){    
            setIsLoading(true);
            const order: Order = new Order("", OrderType.NONE)
            const filters: Filters = new Filters()
            filters.add(textFilter)
            console.log("Current page: ", movieList.pagination.page)
            const criteria: Criteria = new Criteria(filters, order, movieList.pagination.getNextPage())
            
            const movieSearcherRepository = new TheMovieDBRepository()
            console.log("Next page: ", criteria.pagination.page)
            const movieSearcher = new MoviesSearchByCriteria(movieSearcherRepository, criteria)
            //const movieSearcher = new MoviesSearchAll(movieSearcherRepository)
            movieSearcher.search()
              .then(newMovieList => {
                // let concatMovieList : MovieList = {
                //   movies:  movieList.movies.concat(newMovieList.movies),
                //   pagination: newMovieList.pagination
                // }
                // setMoviesList(concatMovieList)
                setMoviesList(newMovieList)
                console.log("Page return: ", newMovieList.pagination.page)
              })
              .catch(err => { console.error(err) })
              .catch(err => { console.error(err) })
        
            setIsLoading(false);
          }
        }
      },
      { threshold: 1 }
    );
  
    if (observerTargetEndPage.current) {
      observer.observe(observerTargetEndPage.current);
    }
  
    return () => {
      if (observerTargetEndPage.current) {
        observer.unobserve(observerTargetEndPage.current);
      }
    };
  }, [textFilter, observerTargetEndPage])

  return {
    movieList,
    textFilter,
    // pagination,
    observerTargetEndPage,
    isLoading,
    setTextFilter,
    // setPagination
  }
}
