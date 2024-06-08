using AutoFixture;
using Shared.Domain.Criteria;

namespace Test.Movies.Domain
{
  public class PaginationFactory
  {
    public static Pagination BuildInitial(){
      Fixture fixture = new Fixture();

      return new Pagination(1, fixture.Create<int>());
    }

    public static Pagination BuildRandom(){
      Fixture fixture = new();
      return fixture.Create<Pagination>();
    }

    
  }
}