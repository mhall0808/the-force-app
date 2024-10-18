import React from 'react';
import './App.css';
import StarshipList from './components/Starship/StarshipList';

const App: React.FC = () => {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Star Wars Starships</h1>
        <StarshipList /> {/* Render the starship list here */}
      </header>
    </div>
  );
}

export default App;
