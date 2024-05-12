namespace Domain.Shared.Criteria.Filters
{
  public sealed class Filter
  {
    public readonly string field;
    public readonly string value;

    public Filter (string field, string value){
      this.field = field;
      this.value = value;
    }

    public FilterOperator fieldOperator {get; set;}
  } 
}