using Movies.Application;
using Movies.Domain;
using Movies.Domain.ValueObjects;
using Moq;
using Test.Movies.Domain;
using Test.Movies.Domain.ValueObjects;
using Movies.Application.MovieFind;
using MovieDTO = Movies.Application.DTO.Movie;
using Movies.Application.DTO.Transforms;

namespace Test.Movies.Application
{
  public class MovieFindByIdTest
  {
    [Fact]
    public async Task FindAMoviWithWrongId(){
      MovieId wrongId = MovieIdFactory.BuildRandomMovieID();
      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.findById(It.IsAny<MovieId>()) == Task.FromResult<Movie?>(null));

      MoviFindById finder = new MoviFindById(moviRepoMok);

      Movie? movieNotFound = await finder.find(wrongId);

      Assert.Null(movieNotFound);
    }

    [Fact]
    public async Task FindAMoviWithGoodId(){
      Movie movie = MovieFactory.BuildRandom();
      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.findById(movie.id) == Task.FromResult<Movie?>(movie));

      MoviFindById finder = new MoviFindById(moviRepoMok);

      Movie? movieMustFound = await finder.find(movie.id);

      Assert.Equal(movie, movieMustFound);
    }
  }
}