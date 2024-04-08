import { type Movie as MovieType } from '../types'
import { Card, CardActionArea, CardContent, CardMedia, Grid, Typography } from '@mui/material'

interface Props {
  movie: MovieType
}

export const Movie: React.FC<Props> = ({ movie }) => {
  return (
    <Grid item>
      <Card sx={{ maxWidth: 345 }}>
        <CardActionArea>
          <CardMedia
            component="img"
            alt="green iguana"
            height="140"
            image={movie.backdrop_path}
          />
          <CardContent>
            <Typography gutterBottom variant="h5" component="div">
              {movie.title}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              {movie.overview}
            </Typography>
          </CardContent>
        </CardActionArea>
      </Card>

      {/* <ImageListItemBar
            title={movie.title}
            subtitle={movie.author}
            actionIcon={
              <IconButton
                sx={{ color: 'rgba(255, 255, 255, 0.54)' }}
                aria-label={`info about ${movie.title}`}
              >
              </IconButton>
            }
          /> */}
    </Grid>
  )
}
