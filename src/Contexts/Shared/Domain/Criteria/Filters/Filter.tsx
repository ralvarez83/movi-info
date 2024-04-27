import { type FilterOperator, type FilterField, type FilterValue } from "./FilterTypes.d"

export class Filter {

  field: FilterField
  operator: FilterOperator
  value: FilterValue

  constructor(field: FilterField, operator: FilterOperator, value: FilterValue) {
    this.field = field
    this.operator = operator
    this.value = value
  }

  /**
   * getField
   */
  public getField(): FilterField {
    return this.field
  }

  public setValue(value: string): void {
    this.value = value
  }

  public getValue(): FilterValue {
    return this.value
  }

  /**
   * getOperator
   */
  public getOperator(): FilterOperator {
    return this.operator
  }

}
