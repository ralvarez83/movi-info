import { useMemo } from 'react'
import {
  Route,
  BrowserRouter as Router,
  Routes
} from 'react-router'
import './App.css'
import { MovieDetails } from './apps/frontend/components/MovieDetails'
import { MovieListInfinite } from './apps/frontend/components/MovieListInfinite'
import { Navbar } from './apps/frontend/components/shared/Navbar'
import { DotNetBackRepository } from './Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository'

export const App: React.FC = () => {
  // El repositorio se crea una sola vez. Sin esto era un objeto nuevo en cada render, y como
  // los hooks lo reciben por parámetro, ninguno podía usarlo como dependencia de un efecto sin
  // provocar un bucle. Es lo que obligaba a dejar las listas de dependencias incompletas.
  const repository : DotNetBackRepository = useMemo(
    () => new DotNetBackRepository(import.meta.env.VITE_DOT_NET_BACK),
    []
  )
  return (
    <>
      <header>
        <Navbar/>
      </header>
      <Router>
        <Routes>
          <Route path="/movie/:id" element={<MovieDetails repository={repository} />} />
          <Route path="/" element={<MovieListInfinite repository={repository} />}/>
        </Routes>
      </Router>
    </>
  )
}

export default App
