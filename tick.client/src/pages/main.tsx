import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './output.css'
import App from './App.js'
import Routing from './Routing'
import './index.css'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
    <Routing />
    <App />
    </StrictMode>,
)
