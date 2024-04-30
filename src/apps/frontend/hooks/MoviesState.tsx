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
  pagination: Pagination,
  isLoading: boolean,
  // observerTargetEndPage: React.MutableRefObject<null>,
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

	// const observerTargetEndPage = useRef(null);

  // useEffect(() => {
  //   const observer = new IntersectionObserver(onIntersection);
    
  //   if(observer && observerTargetEndPage.current) observer.observe(observerTargetEndPage.current);
    
  //   return () => {
  //     if (observer) observer.disconnect();
  //   };
  // }, [movieList])

  useEffect(() => {
    
    setPagination(new Pagination(0))
    setMoviesList([])

  }, [textFilter]);


  // const onIntersection = async(entries:IntersectionObserverEntry[]) => {
  //   const firstEntry = entries[0]
  //   if (firstEntry.isIntersecting && !pagination.isLastPage()) getMovies();
  // }

  const getMovies = () => {
    if (!pagination.isLastPage()){
      setIsLoading(true);
      const order: Order = new Order("", OrderType.NONE)
      const filters: Filters = new Filters()
      filters.add(textFilter)
      console.log("Current page: ", pagination.page)
      const criteria: Criteria = new Criteria(filters, order, pagination.getNextPage())
  
      const movieSearcherRepository = new TheMovieDBRepository()
      console.log("Next page: ", criteria.pagination.page)
      const movieSearcher = new MoviesSearchByCriteria(movieSearcherRepository, criteria)
      movieSearcher.search().then (moviesFound => {
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
    // observerTargetEndPage,
    isLoading,
    setTextFilter,
    getMovies
  }
}
