import React from 'react';

interface NavbarProps {
  entities: any;
  rotatedIcons: { [key: string]: boolean };
  handleIconClick: (fetchFunction: () => void, iconKey: string) => void;
  handleGoClick: () => void;
  isLoading: boolean;
  isCrawlPlaying: boolean;
  fetchRandomPerson: () => void;
  fetchRandomStarship: () => void;
  fetchRandomPlanet: () => void;
  fetchRandomFilm: () => void;
  fetchRandomVehicle: () => void;
  fetchRandomSpecies: () => void;
}

const Navbar: React.FC<NavbarProps> = ({
  entities,
  rotatedIcons,
  handleIconClick,
  handleGoClick,
  isLoading,
  isCrawlPlaying,
  fetchRandomPerson,
  fetchRandomStarship,
  fetchRandomPlanet,
  fetchRandomFilm,
  fetchRandomVehicle,
  fetchRandomSpecies,
}) => (
  <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
    <div className="container-fluid">
      {/* Navbar Brand */}
      <a className="navbar-brand" href="#">
        Star Wars Adventure
      </a>
      {/* Navbar Toggler */}
      <button
        className="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon"></span>
      </button>
      {/* Navbar Content */}
      <div className="collapse navbar-collapse" id="navbarNav">
        <ul className="navbar-nav me-auto">
          <li
            className="nav-item"
            onClick={() => handleIconClick(fetchRandomPerson, 'person')}
          >
            <a className="nav-link" href="#">
              <i className="bi bi-person-fill"></i> {entities.people ? entities.people.name : 'Person'}
            </a>
          </li>
          <li
            className="nav-item"
            onClick={() => handleIconClick(fetchRandomStarship, 'starship')}
          >
            <a className="nav-link" href="#">
              <i className="bi bi-rocket-fill"></i> {entities.starship ? entities.starship.name : 'Starship'}
            </a>
          </li>
          <li
            className="nav-item"
            onClick={() => handleIconClick(fetchRandomPlanet, 'planet')}
          >
            <a className="nav-link" href="#">
              <i className="bi bi-globe"></i> {entities.planet ? entities.planet.name : 'Planet'}
            </a>
          </li>
          <li
            className="nav-item"
            onClick={() => handleIconClick(fetchRandomFilm, 'film')}
          >
            <a className="nav-link" href="#">
              <i className="bi bi-film"></i> {entities.film ? entities.film.title : 'Film'}
            </a>
          </li>
          <li
            className="nav-item"
            onClick={() => handleIconClick(fetchRandomVehicle, 'vehicle')}
          >
            <a className="nav-link" href="#">
              <i className="bi bi-truck"></i> {entities.vehicle ? entities.vehicle.name : 'Vehicle'}
            </a>
          </li>
          <li
            className="nav-item"
            onClick={() => handleIconClick(fetchRandomSpecies, 'species')}
          >
            <a className="nav-link" href="#">
              <i className="bi bi-bug-fill"></i> {entities.species ? entities.species.name : 'Species'}
            </a>
          </li>
        </ul>
        {/* GO Button */}
        <button
          className="btn btn-success"
          onClick={handleGoClick}
          disabled={isLoading || isCrawlPlaying}
        >
          {isLoading ? 'Loading...' : 'GO'}
        </button>
      </div>
    </div>
  </nav>
);

export default Navbar;
