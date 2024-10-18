import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap styles
import './App.css'; // Your custom styles
import StarshipCard from './components/Starship/StarshipCard';
import { fetchAllStarships } from './api/starshipApi';

const App: React.FC = () => {
  const [starships, setStarships] = useState<any[]>([]);
  const [randomStarship, setRandomStarship] = useState<any>(null);

  useEffect(() => {
    const getStarships = async () => {
      const data = await fetchAllStarships(); // Fetch all starships from API
      setStarships(data);
      setRandomStarship(data[Math.floor(Math.random() * data.length)]); // Pick a random starship
    };

    getStarships();
  }, []);

  return (
    <div className="App">
      {/* Navbar Section */}
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
        <div className="container-fluid">
          <a className="navbar-brand" href="#">Starship Types</a>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav ms-auto">
              <li className="nav-item">
                <a className="nav-link" href="#"><i className="bi bi-rocket-fill"></i> Starfighters</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#"><i className="bi bi-globe"></i> Corvettes</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#"><i className="bi bi-truck"></i> Freighters</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#"><i className="bi bi-suit-heart-fill"></i> Cruisers</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#"><i className="bi bi-shield-fill"></i> Battleships</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#"><i className="bi bi-lightning-fill"></i> Destroyers</a>
              </li>
            </ul>
          </div>
        </div>
      </nav>

      {/* Main Content Section */}
      <header className="App-header">
        <h1>Starship Spec Sheet</h1>
        {/* Display the random starship card */}
        {randomStarship ? (
          <StarshipCard starship={randomStarship} />
        ) : (
          <p>Loading...</p>
        )}
      </header>
    </div>
  );
}

export default App;
