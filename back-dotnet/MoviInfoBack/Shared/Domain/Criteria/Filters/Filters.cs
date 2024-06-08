using System.Collections.Immutable;

namespace Shared.Domain.Criteria.Filters
{
  public sealed class Filters
  {
    public ImmutableList<Filter> filtersFiled {get; private set;} = ImmutableList.Create<Filter>();

    public void add(Filter newFilter){
      if (!filtersFiled.Exists((filter) => {
        return filter.field == newFilter.field;
      })){
        filtersFiled = filtersFiled.Add(newFilter);
      }
    }

    public bool hasFilters(){
      return filtersFiled.Count != 0;
    }

  }
}