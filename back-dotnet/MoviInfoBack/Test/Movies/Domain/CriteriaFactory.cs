using AutoFixture;
using Domain.Shared.Criteria;
using Domain.Shared.Criteria.Filters;

namespace Test.Movies.Domain
{
  public class CriteriaFactory
  {
    public static Criteria BuildWithRandomPaginationEmptyFilters (){
      Fixture fixture = new();

      return fixture.Create<Criteria>();
    }

    public static Criteria BuildWithInitialPagination (Filters? filters = null){
      Fixture fixture = new();

      if (null == filters)
        filters = fixture.Create<Filters>();

      Criteria criteria = new Criteria(filters, PaginationFactory.BuildInitial());

      return criteria;
    }
  }
}