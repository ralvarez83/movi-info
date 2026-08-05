import { useEffect, useState } from "react";
import { Movie } from "../../../Contexts/movies/domain/Movie";
import { MovieFindById } from "../../../Contexts/movies/application/MovieFindById";
import { MovieRepository } from "../../../Contexts/movies/domain/MovieRepository";

export type movieDetailsReturn = {
  movie: Movie,
  isLoading: boolean,
  error: string
}

export function useMovieDetails(movieId:string, repository: MovieRepository): movieDetailsReturn {

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
    // movieId y repository faltan a propósito en las dependencias, y eso tiene una consecuencia
    // real: si se navega de una película a otra sin desmontar el componente, no se vuelve a
    // pedir la ficha. Hoy no se nota porque a /movie/:id solo se llega desde el listado, que sí
    // desmonta. Añadirlas requiere antes estabilizar repository, que App.tsx crea nuevo en cada
    // render, o el efecto entraría en bucle.
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  
  return{
    movie,
    isLoading,
    error
  }
}