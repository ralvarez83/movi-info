export type FilterField = string
export type FilterValue = string

export const enum FilterOperator {
  EQUAL = '=',
  NOT_EQUAL = '!=',
  GT = '>',
  LT = '<',
  CONTAINS = 'CONTAINS',
  NOT_CONTAINS = 'NOT_CONTAINS'
}

export type Filter ={
  field: FilterField
  operator: FilterOperator
  value: FilterValue
}