import { useMediaQuery } from "../../hooks/UseMediaQueries";



interface Props {
  percentaje: number,
}

export const VoteCircularPercentaje : React.FC<Props> = ({percentaje}) => {
  
  const isMediaLight = useMediaQuery('(prefers-color-scheme: light)')
  const percentajeVote = Math.trunc(percentaje * 10) 

  // console.log('Porcentaje', percentajeVote)

  // console.log('Light: ', isMediaLight)

  const percentajeVotesLight = {
    backgroundImage: 'conic-gradient(#757575 ' + (100 - percentajeVote) + '%, black 0)'
  };
  const percentajeVotesDark = {
    backgroundImage: 'conic-gradient(#757575 ' + (100 - percentajeVote) + '%, white 0)'
  }
  return(
    <div className="percentajeCircular">
      <div className="circle" style={isMediaLight ? percentajeVotesLight: percentajeVotesDark}>
        <div className="inner" aria-label="Porcentaje de aceptación de la película">{percentajeVote}%</div>
      </div>
    </div>
  )
}