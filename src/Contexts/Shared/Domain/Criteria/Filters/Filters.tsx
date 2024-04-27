import { Filter } from "./Filter";

export class Filters {
  filtersFiled: Filter[] = []

  public add(newFilter: Filter): void{
    if (newFilter instanceof Filter){
      this.filtersFiled = [ 
        ... this.filtersFiled.filter(filter => { return filter.getField() !== newFilter.getField()}),
        newFilter
      ]
    }
  }  

  public hasFilters(): boolean {
    return this.filtersFiled.length === 0
  }

  public getFilters (): Filter[]{
    return this.filtersFiled;
  }

}