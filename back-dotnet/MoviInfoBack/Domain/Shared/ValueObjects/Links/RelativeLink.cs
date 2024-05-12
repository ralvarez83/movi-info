using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects.Links
{
  public partial record RelativeLink: LinkRegex{
    public string value {get; init;}

    private const string _PATTER = "(/){1}\\S+";

    private RelativeLink (string url) => value = url;

    [GeneratedRegex(_PATTER)]
    private static partial Regex RelativeLinkRegex();

    public static LinkRegex Create (string url){
      if (string.IsNullOrEmpty(url) || !RelativeLinkRegex().IsMatch(url))
        return new RelativeLink(String.Empty);

      return new RelativeLink(url);
    }

  }
}