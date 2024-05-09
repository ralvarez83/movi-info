import { useEffect, useState } from "react";
import { Movie } from "../../../Contexts/movies-info/movies/domain/Movie";
import { TheMovieDBRepository } from "../../../Contexts/movies-info/movies/infraestruture/theMovieDb/TheMovieDBRepository";
import { Criteria } from "../../../Contexts/Shared/Domain/Criteria/Criteria";
import { Filter, FilterOperator } from "../../../Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d";
import { Filters } from "../../../Contexts/Shared/Domain/Criteria/Filters/Filters";
import { MovieFindById } from "../../../Contexts/movies-info/movies/application/MovieFindById";

export function movieDetails(movieId:String): {
  movie: Movie,
  isLoading: boolean,
  error: string
} {

  const emptyMovie : Movie = {
    adult: false,
    vertical_image_path: '',
    horizontal_image_path: '',
    title: '',
    overview: '',
    id: ''
  }
  const [movie, setMovie] = useState(emptyMovie);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');
  
  useEffect(() => {
    setIsLoading(true);

    const repository : TheMovieDBRepository = new TheMovieDBRepository()
    const movieFinder : MovieFindById = new MovieFindById(repository, movieId)

    movieFinder.find().then(wantedMovie => {
      if (wantedMovie === undefined)
        setError('Película no encontrada')
      else
        setMovie(wantedMovie)

      setIsLoading(false)
    })

    // setIsLoading(false);
  }, []);
  
  return{
    movie,
    isLoading,
    error
  }
}