import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import Routing from './Routing'

createRoot(document.getElementById('root')).render(
    <StrictMode>
    <Routing />
    <App />
  </StrictMode>,
)
