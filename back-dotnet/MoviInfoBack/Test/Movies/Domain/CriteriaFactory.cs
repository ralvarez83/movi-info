using AutoFixture;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;

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