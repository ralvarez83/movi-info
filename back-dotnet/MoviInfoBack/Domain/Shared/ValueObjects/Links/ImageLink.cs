namespace Domain.Shared.ValueObjects.Links
{
  public partial record ImageLink: LinkRegex{
    private const string VIDEO_ICON = "/video-icon.jpg";

    private const string _PATTER = "(/){1}\\S+";

    public string value { get; init; }
    private ImageLink (string url) => value = url;

    public static LinkRegex Create (string url){
      LinkRegex imageLink = Link.Create(url);
      if (null == imageLink)
        return RelativeLink.Create(VIDEO_ICON);

      return imageLink;
    }

  }
}