interface Props {
  id: number
  title: string
  backdrop_path: string
  overview: string
}

export const Movie: React.FC<Props> = ({ id, title, backdrop_path, overview }) => {

  const truncate = (str: string, n: number): string => {
    return (str.length > n) ? str.slice(0, n-1) + '...' : str;
  };

  return (
    <article>
      <a href={"/movie/" + id}>
        <figure>
          <img src={backdrop_path} />
          <figcaption>{truncate(title, 25)}</figcaption>
        </figure>
        <p>
          {truncate(overview, 100)}
        </p>
        {/* <Grid item>
          <Card sx={{ maxWidth: 345 }}>
            <CardActionArea LinkComponent='a' href={"/movie/" + movie.id}>
              <CardMedia
                component="img"
                alt="green iguana"
                height="140"
                image={movie.backdrop_path}
              />
              <CardContent>
                  <Typography gutterBottom variant="h5" component="div">
                    {truncate(movie.title, 25)}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    {truncate(movie.overview, 100)}
                  </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        </Grid> */}

      </a>
    </article>
  )
}
