using MediatR;
using Movies.Application.Dtos;
using Movies.Application.MovieSearch;

namespace Movies.Infraestructure.MediatR.MovieSearch
{
    public class MediatRMovieSearchByCriteriaQueryHandler(MovieSearchByCriteria movieSearchByCriteria) : MovieSearchByCriteriaQueryHandler(movieSearchByCriteria), IRequestHandler<MediatRMovieSearchByCriteriaQuery, MovieSearchResults>
    {
        public Task<MovieSearchResults> Handle(MediatRMovieSearchByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return this.Handle((MovieSearchByCriteriaQuery) request, cancellationToken);
        }
    }
}