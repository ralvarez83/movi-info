using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects.Links
{
  public partial record Link: LinkRegex{
    public string value {get; init;}

    private const string _PATTER = "((mailto\\:|(news|(ht|f)tp(s?))\\://){1}\\S+)";

    private Link (string url) => value = url;

    [GeneratedRegex(_PATTER)]
    private static partial Regex LinkRegex();

    public static LinkRegex Create (string url){
      if (string.IsNullOrEmpty(url) || !Link.LinkRegex().IsMatch(url))
        return new Link(String.Empty);

      return new Link(url);
    }

  }
}