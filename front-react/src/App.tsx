import {
  Route,
  BrowserRouter as Router,
  Routes
} from 'react-router-dom'
import './App.css'
import { MovieDetails } from './apps/frontend/components/MovieDetails'
import { MovieListInfinite } from './apps/frontend/components/MovieListInfinite'
import { Navbar } from './apps/frontend/components/shared/Navbar'
import { DotNetBackRepository } from './Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository'

export const App: React.FC = () => {
  const repository : DotNetBackRepository = new DotNetBackRepository(import.meta.env.VITE_DOT_NET_BACK)
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
