namespace Domain.Shared.Criteria
{
  public class Pagination
  {    
    public int? pageSize {get; set;}
    private int _page;
    private int _totalPage;

    public Pagination(int page, int totalPage){
      this._page = page;
      this._totalPage = totalPage;
    }

    public bool isLastPage(){
      return this._totalPage == this._page;
    }

    public Pagination getNextPage(){
      return (this.isLastPage())? this: new Pagination(this._page +1, this._totalPage);
    }
  }
}