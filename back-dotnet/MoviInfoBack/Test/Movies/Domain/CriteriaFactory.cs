using AutoFixture;
using Domain.Shared.Criteria;

namespace Test.Movies.Domain
{
  public class CriteriaFactory
  {
    public static Criteria BuildRandom (){
      Fixture fixture = new Fixture();

      return fixture.Create<Criteria>();
    }
  }
}