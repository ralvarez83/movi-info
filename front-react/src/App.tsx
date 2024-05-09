import {
  Route,
  BrowserRouter as Router,
  Routes
} from 'react-router-dom'
import './App.css'
import { MovieDetails } from './apps/frontend/components/MovieDetails'
import { MoviesList } from './apps/frontend/components/MovieList'
import { Navbar } from './components/Navbar'
import { MovieListInfinite } from './apps/frontend/components/MovieListInfinite'
import { DevFooter } from './apps/frontend/components/shared/DevFooter'

export const App: React.FC = () => {
  return (
    <>
      <header>
        <Navbar/>
      </header>
      <Router>
        <Routes>
          <Route path="/movie/:id" element={<MovieDetails />} />
          <Route path="/" element={<MovieListInfinite />}/>
        </Routes>
      </Router>
    </>
  )
}

export default App
