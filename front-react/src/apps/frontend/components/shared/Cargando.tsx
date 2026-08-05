// React 19 retiró el namespace global JSX de @types/react: ahora se importa desde 'react'.
import type { JSX } from 'react'

export const Cargando = (): JSX.Element => {
  return (
    <div className='cargando'>
      <aside></aside>
      <label>Cargando</label>
    </div>
  )
}

