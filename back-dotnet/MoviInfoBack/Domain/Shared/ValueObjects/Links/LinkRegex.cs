namespace Domain.Shared.ValueObjects.Links
{
  public interface LinkRegex
  {
    public abstract static LinkRegex Create(string url);
  }
}