import { CssBaseline } from '@mui/material'
import {
  BrowserRouter as Router,
  Route,
  Routes
} from 'react-router-dom'
import { MoviesList } from './components/MovieList'
import { MovieDetails } from './components/MovieDetails'

export const App: React.FC = () => {
  return (
    <>
      <CssBaseline />
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
