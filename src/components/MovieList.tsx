import { Box, Grid, Typography } from '@mui/material'
import { Movie } from './Movie'
import { useMovies } from '../hooks/useMovies'
import { wrap } from 'module'

export const MoviesList = (): JSX.Element => {
  const {
    movies
  } = useMovies()

  return (
    <>
       <Box>
        <Typography variant="h1" gutterBottom align={'center'}>
          Movi - INFO
        </Typography>
      </Box>
      <Grid sx={{ flexGrow: 1}} container={true} spacing={2} className='MuiGrid-root' justifyContent={'space-around'}>
        {movies.map((movie) => (
            <Movie key={movie.id} movie={movie} />
          ))}
      </Grid>
    </>
  )
}
