using MediatR;
using Movies.Application.Dtos;
using Movies.Application.MovieFind;

namespace Movies.Infraestructure.MediatR.MovieFind
{
    public class MediatRFindByIdMovieQueryHandler(MovieFindById movieFindById) : FindByIdMovieQueryHandler(movieFindById), IRequestHandler<MediatRFindbyIdMovieQuery, Movie?>
    {
        public Task<Movie?> Handle(MediatRFindbyIdMovieQuery request, CancellationToken cancellationToken)
        {
            return this.Handle((FindByIdMovieQuery) request, cancellationToken);
        }
    }
}