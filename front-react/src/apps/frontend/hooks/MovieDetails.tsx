import { useEffect, useState } from "react";
import { Movie } from "../../../Contexts/movies/domain/Movie";
import { MovieFindById } from "../../../Contexts/movies/application/MovieFindById";
import { DotNetBackRepository } from "../../../Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository";
import { MovieRepository } from "../../../Contexts/movies/domain/MovieRepository";

export type movieDetailsReturn = {
  movie: Movie,
  isLoading: boolean,
  error: string
}

export function movieDetails(movieId:String, repository: MovieRepository): movieDetailsReturn {

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