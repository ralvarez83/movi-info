using AutoFixture;
using Domain.Shared.Criteria;

namespace Test.Movies.Domain
{
  public class PaginationFactory
  {
    public static Pagination BuildInitial(){
      Fixture fixture = new Fixture();

      return new Pagination(1, fixture.Create<int>());
    }

    
  }
}