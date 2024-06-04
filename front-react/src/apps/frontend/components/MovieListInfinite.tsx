"use client";
import { moviesState } from '../hooks/MoviesState'
import { Movie } from './Movie';
import { TextFilter } from './shared/TextFilter';
import { DevFooter } from './shared/DevFooter';
import { InfinitePagination } from './shared/InfinitePagination';
import { MovieRepository } from '../../../Contexts/movies/domain/MovieRepository';
import { Cargando } from './shared/Cargando';
interface Props {
  repository: MovieRepository
}

export const MovieListInfinite: React.FC<Props> = ({repository}) => {
  const {
    movieList,
    textFilter,
		isLoading,
		pagination,
    setTextFilter,
		getMovies
  } = moviesState(repository)
	
	return (
		<main className='movie-list'>
		<h2>Todas las películas</h2>
		<div>
			<TextFilter filter={textFilter} placeholder='Busca por texto...' setFilter={setTextFilter} />
		</div>
		<InfinitePagination dataList={movieList} getMoreData={getMovies}>
			{(!isLoading || (isLoading && movieList.length > 0)) && 
				<section>
					{movieList.map((movie) => (
							<Movie key={pagination.page + "-" + movie.id} image_path={movie.horizontalImagePath} {... movie} />
						))}
				</section>
			}
			{isLoading && <Cargando />}
		</InfinitePagination>
		<DevFooter pagination={pagination} />
	</main>
	);
};

