import { Filter } from "../../../../Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d"

interface Props {
  filter: Filter
  placeholder: string
  setFilter: React.Dispatch<React.SetStateAction<Filter>>,
  minSearchLenght?: number
}

export const TextFilter: React.FC<Props> = ({ filter, placeholder, setFilter, minSearchLenght = 3}) => {

  const setFilterValue = (value:string): void => {
    if (value.length >= minSearchLenght || value.length < filter.value.length){
      const newFilter : Filter = {
        ... filter,
        value
      }
      setFilter(newFilter)
    }
  }

  return (
    <aside className="textFilter">
      <label htmlFor={filter.field.toString()}>{placeholder}</label>
      <input name={filter.field.toString()} id={filter.field.toString()} placeholder={placeholder} onChange={(e) => setFilterValue(e.target.value)} />
    </aside>
  )
}