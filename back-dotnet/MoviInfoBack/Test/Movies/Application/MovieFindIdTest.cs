using Application.Movies;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using Infraestructure.TheMovieDb;
using Moq;
using Test.Movies.Domain.ValueObjects;

namespace Test.Movies.Application
{
  [TestClass]
  public class MovieFindByIdTest
  {
    [TestMethod]
    public async Task FindAMoviWithWrongId(){
      MovieId wrongId = MovieIdFactory.MovieIDRandomBuild();
      MovieRepository moviRepoMok = Mock.Of<MovieRepository>(_ => _.findById(It.IsAny<MovieId>()) == Task.FromResult<Movie?>(null));

      MoviFindById finder = new MoviFindById(moviRepoMok, wrongId);

      Movie movieNotFound = await finder.find();

      Assert.IsNull(movieNotFound);
    }
  }
}