namespace Domain.Shared.Criteria
{
  public sealed class Pagination
  {    
    public int? pageSize {get; set;}
    public int page {get; private set;}
    public int _totalPage;

    public Pagination(int page, int totalPage){
      this.page = page;
      _totalPage = totalPage;
    }

    public bool isLastPage(){
      return _totalPage == page;
    }

    public Pagination getNextPage(){
      return isLastPage()? this: new Pagination(page +1, _totalPage);
    }
  }
}