import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import Dashboard from './components/Dashboard';
import SearchBar from './components/SearchBar';
import CreateTaskModal from './components/CreateTaskModal';
import UpdateTaskModal from './components/UpdateTaskModal';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Dashboard />
  </StrictMode>
);
