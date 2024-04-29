"use client";
import { useEffect, useRef } from 'react';
import { moviesState } from '../hooks/MoviesState'
import { Movie } from './Movie';
import { TextFilter } from './shared/TextFilter';
import { Pagination } from '../../../Contexts/Shared/Domain/Criteria/Pagination';
import { DevFooter } from './shared/DevFooter';

export const MovieListInfinite = (): JSX.Element => {
  const {
    movieList,
    textFilter,
		isLoading,
		pagination,
		observerTargetEndPage,
    setTextFilter
  } = moviesState()
	
	return (
		<main className='movie-list'>
		<h2>Todas las películas</h2>
		<aside>
			<TextFilter filter={textFilter} placeholder='Busca por texto...' setFilter={setTextFilter} />
		</aside>
		<section>
			{movieList.map((movie) => (
					<Movie key={movie.id} image_path={movie.horizontal_image_path} {... movie} />
				))}

			{isLoading && <p>Cargando...</p>}
		</section>
    <div ref={observerTargetEndPage}></div>
		<DevFooter pagination={pagination} />
	</main>
	);
};
