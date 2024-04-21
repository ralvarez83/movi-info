import {
  BrowserRouter as Router,
  Route,
  Routes
} from 'react-router-dom'
import { MoviesList } from './components/MovieList'
import { MovieDetails } from './components/MovieDetails'
import './App.css'

export const App: React.FC = () => {
  return (
    <>
      <header>
        <h1 className="main-title">
          Movi - INFO
        </h1>
      </header>
      <Router>
        <Routes>
          <Route path="/movie/:id" element={<MovieDetails />} />
          <Route path="/" element={<MoviesList />}/>
        </Routes>
      </Router>
    </>
  )
}

export default App
