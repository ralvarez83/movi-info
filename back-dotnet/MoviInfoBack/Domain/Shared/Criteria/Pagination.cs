namespace Domain.Shared.Criteria
{
  public sealed class Pagination
  {    
    public int? pageSize {get; set;}
    public int page {get; private set;}
    public int totalPage {get; init;}

    public Pagination(int page, int totalPage){
      this.page = page;
      this.totalPage = totalPage;
    }

    public bool isLastPage(){
      return totalPage == page;
    }

    public Pagination getNextPage(){
      return isLastPage()? this: new Pagination(page +1, totalPage);
    }
  }
}