import { useParams } from "react-router-dom";
import { movieDetails } from "../hooks/MovieDetails";

export const MovieDetails = (): JSX.Element => {

  const {id} = useParams();

  const {
    movie,
    isLoading,
    error
  } = movieDetails(new String(id))

  return(
    <main className="movie-details">
      <article>
        <figure>
          <img src={movie.horizontal_image_path} />
        </figure>
        <h2>{movie.title}</h2>
      </article>
      {isLoading && 
				<aside className='cargando'></aside>}
      { error !== '' &&
        <label className = 'errorMessage'>{error}</label>
      }
    </main>
  )
}