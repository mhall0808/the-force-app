// src/App.tsx

import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css'; // Your custom styles
import axios from 'axios';
import StarWarsCrawl from './components/StarWarsCrawl';

// Define the interface for the paragraph response
interface ParagraphResponse {
  paragraphText: string;
}

const App: React.FC = () => {
  const [people, setPeople] = useState<any>(null);
  const [starship, setStarship] = useState<any>(null);
  const [planet, setPlanet] = useState<any>(null);
  const [film, setFilm] = useState<any>(null);
  const [vehicle, setVehicle] = useState<any>(null);
  const [species, setSpecies] = useState<any>(null);
  const [isCrawlPlaying, setIsCrawlPlaying] = useState(false);
  const [crawlText, setCrawlText] = useState<string[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isMobile, setIsMobile] = useState<boolean>(false); // Detect if mobile

  // Fetch the random entities on mount
  useEffect(() => {
    fetchRandomEntities();
  }, []);

  // Resize handler for mobile responsiveness
  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= 576); // Adjust this breakpoint if needed
    };

    window.addEventListener('resize', handleResize);
    handleResize(); // Initial check

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  // Fetch all random entities
  const fetchRandomEntities = () => {
    fetchRandomPerson();
    fetchRandomStarship();
    fetchRandomPlanet();
    fetchRandomFilm();
    fetchRandomVehicle();
    fetchRandomSpecies();
  };

  const fetchRandomPerson = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/people');
      setPeople(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const fetchRandomStarship = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/starships');
      setStarship(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const fetchRandomPlanet = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/planets');
      setPlanet(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const fetchRandomFilm = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/films');
      setFilm(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const fetchRandomVehicle = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/vehicles');
      setVehicle(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const fetchRandomSpecies = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/species');
      setSpecies(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleGoClick = async () => {
    setIsLoading(true);

    const selectedData = {
      Person: people,
      Starship: starship,
      Planet: planet,
      Film: film,
      Vehicle: vehicle,
      Species: species,
    };

    // Fetch paragraphs first
    const paragraphs = [];

    for (let i = 1; i <= 4; i++) {
      try {
        const response = await axios.post<ParagraphResponse>('http://localhost:5000/api/ai/generate-crawl-paragraph', {
          selectedData,
          paragraphNumber: i,
        });
        paragraphs.push(response.data.paragraphText);
      } catch (error) {
        console.error(error);
      }
    }

    // Set the crawl text
    const initialCrawlText = [`${film ? film.title : 'Film'}`, ...paragraphs];
    setCrawlText(initialCrawlText);

    // Start playing the crawl
    setIsCrawlPlaying(true);

    setIsLoading(false);
  };

  const handleSkipCrawl = () => {
    setIsCrawlPlaying(false);
    setCrawlText([]);
    setIsLoading(false); // Reset loading state when crawl is skipped
  };

  // Conditionally render mobile view or desktop view
  return (
    <div className={`App ${isCrawlPlaying ? 'starry-background' : 'flat-background'}`}>
      {isMobile ? (
        <div className="mobile-view">
          {/* Mobile-specific content */}
          <div className="container mt-4">
            <div className="row">
              {/* First row with three icons */}
              <div className="col-4 text-center" onClick={fetchRandomPerson}>
                <i className="bi bi-person-fill icon"></i>
                <p>{people ? people.name : 'Person'}</p>
              </div>
              <div className="col-4 text-center" onClick={fetchRandomStarship}>
                <i className="bi bi-rocket-fill icon"></i>
                <p>{starship ? starship.name : 'Starship'}</p>
              </div>
              <div className="col-4 text-center" onClick={fetchRandomPlanet}>
                <i className="bi bi-globe icon"></i>
                <p>{planet ? planet.name : 'Planet'}</p>
              </div>
            </div>
            <div className="row mt-3">
              {/* Second row with three icons */}
              <div className="col-4 text-center" onClick={fetchRandomFilm}>
                <i className="bi bi-film icon"></i>
                <p>{film ? film.title : 'Film'}</p>
              </div>
              <div className="col-4 text-center" onClick={fetchRandomVehicle}>
                <i className="bi bi-truck icon"></i>
                <p>{vehicle ? vehicle.name : 'Vehicle'}</p>
              </div>
              <div className="col-4 text-center" onClick={fetchRandomSpecies}>
                <i className="bi bi-bug-fill icon"></i>
                <p>{species ? species.name : 'Species'}</p>
              </div>
            </div>
          </div>
          {/* GO Button */}
          <div className="go-button-container">
            <button className="btn btn-success w-100" onClick={handleGoClick} disabled={isLoading}>
              {isLoading ? 'Loading...' : 'GO'}
            </button>
          </div>

          {/* Star Wars Crawl */}
          {isCrawlPlaying && (
            <StarWarsCrawl text={crawlText} onSkip={handleSkipCrawl} duration={40} />
          )}

          {/* Main Content */}
          {!isCrawlPlaying && !isLoading && (
            <div className="main-content">
              <h1>Embark on a Galactic Adventure!</h1>
              <p>Select your characters and click 'GO' to begin.</p>
            </div>
          )}

          {isLoading && !isCrawlPlaying && (
            <div className="loading-indicator">
              <p>Loading your Star Wars adventure...</p>
            </div>
          )}
        </div>
      ) : (
        <div className="desktop-view">
          {/* Navbar Section */}
          <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container-fluid">
              {/* Navbar Brand */}
              <a className="navbar-brand" href="#">Star Wars Adventure</a>
              {/* Navbar Toggler */}
              <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
                      aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
              </button>
              {/* Navbar Content */}
              <div className="collapse navbar-collapse" id="navbarNav">
                <ul className="navbar-nav me-auto">
                  <li className="nav-item" onClick={fetchRandomPerson}>
                    <a className="nav-link" href="#">
                      <i className="bi bi-person-fill"></i> {people ? people.name : 'Person'}
                    </a>
                  </li>
                  <li className="nav-item" onClick={fetchRandomStarship}>
                    <a className="nav-link" href="#">
                      <i className="bi bi-rocket-fill"></i> {starship ? starship.name : 'Starship'}
                    </a>
                  </li>
                  <li className="nav-item" onClick={fetchRandomPlanet}>
                    <a className="nav-link" href="#">
                      <i className="bi bi-globe"></i> {planet ? planet.name : 'Planet'}
                    </a>
                  </li>
                  <li className="nav-item" onClick={fetchRandomFilm}>
                    <a className="nav-link" href="#">
                      <i className="bi bi-film"></i> {film ? film.title : 'Film'}
                    </a>
                  </li>
                  <li className="nav-item" onClick={fetchRandomVehicle}>
                    <a className="nav-link" href="#">
                      <i className="bi bi-truck"></i> {vehicle ? vehicle.name : 'Vehicle'}
                    </a>
                  </li>
                  <li className="nav-item" onClick={fetchRandomSpecies}>
                    <a className="nav-link" href="#">
                      <i className="bi bi-bug-fill"></i> {species ? species.name : 'Species'}
                    </a>
                  </li>
                </ul>
                {/* GO Button */}
                <button className="btn btn-success my-2 my-lg-0" onClick={handleGoClick} disabled={isLoading}>
                  {isLoading ? 'Loading...' : 'GO'}
                </button>
              </div>
            </div>
          </nav>

          {/* Star Wars Crawl */}
          {isCrawlPlaying && (
            <StarWarsCrawl text={crawlText} onSkip={handleSkipCrawl} duration={40} />
          )}

          {/* Main Content */}
          {!isCrawlPlaying && (
            <div className="main-content">
              {isLoading ? (
                <div className="loading-indicator">
                  <p>Loading your Star Wars adventure...</p>
                </div>
              ) : (
                <div className="welcome-content">
                  <h1>Embark on a Galactic Adventure!</h1>
                  <p>Select your characters and click 'GO' to begin.</p>
                </div>
              )}
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default App;
