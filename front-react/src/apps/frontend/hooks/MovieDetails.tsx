import { useEffect, useState } from "react";
import { Movie } from "../../../Contexts/movies-info/movies/domain/Movie";
import { TheMovieDBRepository } from "../../../Contexts/movies-info/movies/infraestruture/theMovieDb/TheMovieDBRepository";
import { MovieFindById } from "../../../Contexts/movies-info/movies/application/MovieFindById";

export function movieDetails(movieId:String): {
  movie: Movie,
  isLoading: boolean,
  error: string
} {

  const emptyMovie : Movie = {
    adult: false,
    verticalImagePath: '',
    horizontalImagePath: '',
    title: '',
    overview: '',
    id: '',
    imdbLink: '',
    usersVote: 0
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