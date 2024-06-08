namespace Shared.Domain.Criteria.Order
{
  public sealed class Order
  {
    public readonly string orderBy;
    public readonly OrderType orderType;

    public Order(string orderBy, OrderType orderType){
      this.orderBy = orderBy;
      this.orderType = orderType;
    }

    public bool isNone(){
      return this.orderType == OrderType.NONE;
    }
  }
}