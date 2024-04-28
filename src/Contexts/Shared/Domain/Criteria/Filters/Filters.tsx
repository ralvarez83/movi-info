import { type Filter } from "./FilterTypes.d";

export class Filters {
  filtersFiled: Filter[] = []

  public add(newFilter: Filter): void{
    this.filtersFiled = [ 
      ... this.filtersFiled.filter(filter => { return filter.field !== newFilter.field}),
      newFilter
    ]
  }  

  public hasFilters(): boolean {
    return this.filtersFiled.length === 0
  }

  public getFilters (): Filter[]{
    return this.filtersFiled;
  }

}