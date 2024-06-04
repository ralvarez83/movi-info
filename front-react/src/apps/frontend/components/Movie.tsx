interface Props {
  id: string
  title: string
  image_path: string
  overview: string
}

export const Movie: React.FC<Props> = ({ id, title, image_path, overview }) => {

  const truncate = (str: string, n: number): string => {
    return (str.length > n) ? str.slice(0, n-1) + '...' : str;
  };

  return (
    <article>
      <a href={"/movie/" + id} aria-label={title} >
        <figure>
          <img src={image_path} alt={title} />
          <figcaption>{truncate(title, 25)}</figcaption>
        </figure>
        <p>
          {truncate(overview, 100)}
        </p>
      </a>
    </article>
  )
}
