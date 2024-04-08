import { type Movie as MovieType } from '../types'
import { IconButton, ImageListItem, ImageListItemBar } from '@mui/material'

interface Props {
  movie: MovieType
}

export const Movie: React.FC<Props> = ({ movie }) => {
  return (
    <ImageListItem key={movie.id}>
      <img
          srcSet={`https://tse4.mm.bing.net/th?id=OIP.2i_sj6KVnFiP263JidPW8gHaHa&pid=Api?w=248&fit=crop&auto=format&dpr=2 2x`}
          src={`https://tse4.mm.bing.net/th?id=OIP.2i_sj6KVnFiP263JidPW8gHaHa&pid=Api?w=248&fit=crop&auto=format`}
          alt={movie.title}
          loading="lazy"
        />
      
      <ImageListItemBar
            title={movie.title}
            subtitle={movie.author}
            actionIcon={
              <IconButton
                sx={{ color: 'rgba(255, 255, 255, 0.54)' }}
                aria-label={`info about ${movie.title}`}
              >
              </IconButton>
            }
          />
    </ImageListItem>
  )
}
