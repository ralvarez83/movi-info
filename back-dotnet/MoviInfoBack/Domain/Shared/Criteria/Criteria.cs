
using Domain.Shared.Criteria.Filters;

namespace Domain.Shared.Criteria
{
  public sealed class Criteria
  {
    public readonly Filters.Filters filters;
    public readonly Order.Order order;
    public readonly Pagination pagination;

    public Criteria( Filters.Filters filters, Order.Order order, Pagination pagination){
      this.filters = filters;
      this.order = order;
      this.pagination = pagination;
    }
    public Criteria( Filters.Filters filters, Pagination pagination){
      this.filters = filters;
      this.pagination = pagination;
      this.order = new Order.Order("",Order.OrderType.NONE);
    }
  }
}