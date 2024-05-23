namespace Domain.Shared.Criteria.Filters
{
  public sealed class Filter
  {
    public static string FILTER_BY_TEXT = "byText";
    public readonly string field;
    public readonly string value;

    public Filter (string field, string value, FilterOperator fieldOperator){
      this.field = field;
      this.value = value;
      this.fieldOperator = fieldOperator;
    }

    public FilterOperator fieldOperator {get; set;}
  } 
}