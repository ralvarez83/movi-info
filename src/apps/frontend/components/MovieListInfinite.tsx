"use client";
import { moviesState } from '../hooks/MoviesState'
import { Movie } from './Movie';
import { TextFilter } from './shared/TextFilter';
import { DevFooter } from './shared/DevFooter';
import { InfinitePagination } from './shared/InfinitePagination';

export const MovieListInfinite = (): JSX.Element => {
  const {
    movieList,
    textFilter,
		isLoading,
		pagination,
    setTextFilter,
		getMovies
  } = moviesState()
	
	return (
		<main className='movie-list'>
		<h2>Todas las películas</h2>
		<aside>
			<TextFilter filter={textFilter} placeholder='Busca por texto...' setFilter={setTextFilter} />
		</aside>
		<InfinitePagination dataList={movieList} getMoreData={getMovies}>
			<section>
				{movieList.map((movie) => (
						<Movie key={pagination.page + "-" + movie.id} image_path={movie.horizontal_image_path} {... movie} />
					))}
			</section>
				{isLoading && 
				<aside className='cargando'></aside>}
		</InfinitePagination>
		<DevFooter pagination={pagination} />
	</main>
	);
};

