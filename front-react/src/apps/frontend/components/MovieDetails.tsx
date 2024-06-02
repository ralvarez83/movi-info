import { useParams } from "react-router-dom";
import { movieDetails } from "../hooks/MovieDetails";
import { VoteCircularPercentaje } from "./shared/VoteCircularPercentaje";
import { MovieRepository } from "../../../Contexts/movies/domain/MovieRepository";
import { Cargando } from "./shared/Cargando";

interface Props {
  repository: MovieRepository
}

export const MovieDetails: React.FC<Props> = ({repository}) => {

  const {id} = useParams();

  const {
    movie,
    isLoading,
    error
  } = movieDetails(new String(id), repository)


  return(
    <main className="movie-details">
      {!isLoading && 
        <section>
          <figure>
            <img src={movie.horizontalImagePath} alt={movie.title} />
          </figure>
          <h2>{movie.title}</h2>
          <article>
            <p>
              {movie.overview}
            </p>
            <div className="more-info">
              <div>
                <label>Acceso a la Web de IMDB</label>
                <a href={movie.imdbLink} target="_blank" aria-label="Link de acceso a la página de IMDB de la película">
                  <img src="/IMDB_Logo_2016.png" alt="logotipo de la página web IMDB" />
                </a>
              </div>
              <div>
                <label>Voto del espectador</label>
                <VoteCircularPercentaje percentaje={movie.usersVote}/>
              </div>
            </div>
          </article>
        </section>
      }
      {isLoading && 
				<Cargando />}
      { error !== '' &&
        <label className = 'errorMessage'>{error}</label>
      }
    </main>
  )
}