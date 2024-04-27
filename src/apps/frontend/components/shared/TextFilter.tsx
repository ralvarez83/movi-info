
interface Props {
  name: string
  placeholder: string
  setFilterValue: (value:string) => void
}

export const TextFilter: React.FC<Props> = ({ name, placeholder, setFilterValue}) => {

  return (
    <>
      <input name={name} placeholder={placeholder} onChange={(e) => setFilterValue(e.target.value)} />
    </>
  )
}