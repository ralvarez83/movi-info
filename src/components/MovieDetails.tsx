import { useParams } from "react-router-dom";

export const MovieDetails = (): JSX.Element => {

  const {id} = useParams();

  return(
    <main className="movie-details">
      <h2>{id}</h2>
      
    </main>
  )
}