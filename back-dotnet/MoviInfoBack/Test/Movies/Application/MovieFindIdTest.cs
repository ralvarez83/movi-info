using Application.Movies;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using Moq;
using Test.Movies.Domain;
using Test.Movies.Domain.ValueObjects;

namespace Test.Movies.Application
{
  public class MovieFindByIdTest
  {
    [Fact]
    public async Task FindAMoviWithWrongId(){
      MovieId wrongId = MovieIdFactory.BuildRandomMovieID();
      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.findById(It.IsAny<MovieId>()) == Task.FromResult<Movie?>(null));

      MoviFindById finder = new MoviFindById(moviRepoMok, wrongId);

      Movie movieNotFound = await finder.find();

      Assert.Null(movieNotFound);
    }

    [Fact]
    public async Task FindAMoviWithGoodId(){
      Movie movie = MovieFactory.BuildRandom();
      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.findById(movie.id) == Task.FromResult<Movie?>(movie));

      MoviFindById finder = new MoviFindById(moviRepoMok, movie.id);

      Movie movieMustFound = await finder.find();

      Assert.Same(movie, movieMustFound);
    }
  }
}