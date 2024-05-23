namespace Domain.Shared.ValueObjects.Links
{
  public interface LinkRegex
  {
    public string value {get; init;}
    public abstract static LinkRegex Create(string url);
  }
}