"use client";
import { useEffect, useRef } from 'react';
import { moviesState } from '../hooks/MoviesState'
import { Movie } from './Movie';
import { TextFilter } from './shared/TextFilter';
import { Pagination } from '../../../Contexts/Shared/Domain/Criteria/Pagination';

export const MovieListInfinite = (): JSX.Element => {
  const {
    movieList,
    textFilter,
		pagination,
		isLoading,
    setTextFilter,
		setPagination
  } = moviesState()

	const observerTarget = useRef(null);


	useEffect(() => {
		const observer = new IntersectionObserver(
			entries => {
				if (entries[0].isIntersecting) {
					if(!movieList.pagination.isLastPage()){
						console.log("Page loaded: ", movieList.pagination.page)
						console.log("Total pages: ", movieList.pagination.totalPage)
						const newPagination: Pagination = movieList.pagination.getNextPage()
						console.log("Page loading: ", newPagination.page)
						setPagination(newPagination)
					}
				}
			},
			{ threshold: 1 }
		);
	
		if (observerTarget.current) {
			observer.observe(observerTarget.current);
		}
	
		return () => {
			if (observerTarget.current) {
				observer.unobserve(observerTarget.current);
			}
		};
	}, [observerTarget]);
	
	return (
		<main className='movie-list'>
		<h2>Todas las películas</h2>
		<aside>
			<TextFilter filter={textFilter} placeholder='Busca por texto...' setFilter={setTextFilter} />
		</aside>
		<section>
			{movieList.movies.map((movie) => (
					<Movie key={movie.id} image_path={movie.horizontal_image_path} {... movie} />
				))}

			{isLoading && <p>Cargando...</p>}
    <div ref={observerTarget}>{movieList.pagination.page}-{movieList.pagination.totalPage}</div>
		</section>
	</main>
	);
};

