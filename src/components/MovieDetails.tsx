import { useParams } from "react-router-dom";

export const MovieDetails = (): JSX.Element => {

  const {id} = useParams();

  return(
    <>{id}</>
  )
}