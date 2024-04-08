import { type Movie as MovieType } from '../types'
import { IconButton, ImageListItem, ImageListItemBar } from '@mui/material'

interface Props {
  movie: MovieType
}

export const Movie: React.FC<Props> = ({ movie }) => {
  return (
    <ImageListItem key={movie.id}>
      <img
          srcSet={movie.poster_path}
          src={movie.poster_path}
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
