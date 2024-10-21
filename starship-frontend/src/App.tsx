// src/App.tsx

import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css'; // Import Bootstrap Icons
import './App.css'; // Your custom styles
import axios from 'axios';
import StarWarsCrawl from './components/StarWarsCrawl';

// Define the interface for the paragraph response
interface ParagraphResponse {
  paragraphText: string;
}

const App: React.FC = () => {
  // State variables
  const [people, setPeople] = useState<any>(null);
  const [starship, setStarship] = useState<any>(null);
  const [planet, setPlanet] = useState<any>(null);
  const [film, setFilm] = useState<any>(null);
  const [vehicle, setVehicle] = useState<any>(null);
  const [species, setSpecies] = useState<any>(null);
  const [isCrawlPlaying, setIsCrawlPlaying] = useState(false);
  const [crawlText, setCrawlText] = useState<string[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isMobile, setIsMobile] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [rotatedIcons, setRotatedIcons] = useState<{ [key: string]: boolean }>({});

  // Fetch the random entities on mount
  useEffect(() => {
    fetchRandomEntities();
  }, []);

  // Resize handler for mobile responsiveness
  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= 576);
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

  // Sanitize selected data
  const sanitizeSelectedData = (data: any): any => {
    if (typeof data === 'string') {
      return data.replace(/slave/gi, 'servant');
    } else if (Array.isArray(data)) {
      return data.map(sanitizeSelectedData);
    } else if (typeof data === 'object' && data !== null) {
      const sanitizedObject: any = {};
      for (const key in data) {
        sanitizedObject[key] = sanitizeSelectedData(data[key]);
      }
      return sanitizedObject;
    } else {
      return data;
    }
  };

  // Fetch functions
  const fetchRandomPerson = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/people');
      setPeople(response.data);
    } catch (error) {
      console.error(error);
      setError('Failed to load Person data.');
    }
  };

  const fetchRandomStarship = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/starships');
      setStarship(response.data);
    } catch (error) {
      console.error(error);
      setError('Failed to load Starship data.');
    }
  };

  const fetchRandomPlanet = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/planets');
      setPlanet(response.data);
    } catch (error) {
      console.error(error);
      setError('Failed to load Planet data.');
    }
  };

  const fetchRandomFilm = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/films');
      setFilm(response.data);
    } catch (error) {
      console.error(error);
      setError('Failed to load Film data.');
    }
  };

  const fetchRandomVehicle = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/vehicles');
      setVehicle(response.data);
    } catch (error) {
      console.error(error);
      setError('Failed to load Vehicle data.');
    }
  };

  const fetchRandomSpecies = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/random/species');
      setSpecies(response.data);
    } catch (error) {
      console.error(error);
      setError('Failed to load Species data.');
    }
  };

  const handleGoClick = async () => {
    setIsLoading(true);
    setError(null); // Reset any previous errors

    const selectedData = {
      Person: people,
      Starship: starship,
      Planet: planet,
      Film: film,
      Vehicle: vehicle,
      Species: species,
    };

    // Sanitize the selected data
    const sanitizedData = sanitizeSelectedData(selectedData);

    // Predefined paragraphs with more interesting content
    const paragraph1 =
      "Turmoil has engulfed the Galactic Republic. The sinister Darth Lord Mark, cloaked in darkness, spreads treachery and chaos across the stars, leaving entire worlds trembling in fear.";
    const paragraph2 = `But heroes rise in the unlikeliest of places. On the remote planet ${sanitizedData.Planet.name}, the courageous GoEngineer rebellion, led by Vitali, Royce, and Francisco, battles valiantly to protect the last of the noble ${sanitizedData.Species.name}.`;
    const paragraph4 = `Unaware of the impending danger, they continue their fight. Yet, the malevolent Darth Lord Mark races towards them aboard his dreaded starship, the ${sanitizedData.Starship.name}. Only ${sanitizedData.Person.name}, armed with the legendary ${sanitizedData.Vehicle.name}, stands between hope and annihilation.`;

    try {
      // Show loading screen
      setIsCrawlPlaying(false);
      setCrawlText([]);
      setIsLoading(true);

      // Fetch paragraph 3 asynchronously
      const response = await axios.post<ParagraphResponse>(
        'http://localhost:5000/api/ai/generate-crawl-paragraph',
        {
          selectedData: sanitizedData,
          paragraphNumber: 3,
        }
      );
      const paragraph3Text = response.data.paragraphText;

      // Once all paragraphs are ready, set the crawl text and start the crawl
      const crawlContent = [
        film ? film.title.toUpperCase() : 'STAR WARS', // Film title at the top
        paragraph1, // Paragraph 1
        paragraph2, // Paragraph 2
        paragraph3Text, // Paragraph 3
        paragraph4, // Paragraph 4
      ];
      setCrawlText(crawlContent);

      // Start the crawl
      setIsCrawlPlaying(true);
    } catch (error) {
      console.error(error);
      setError('Failed to load crawl paragraph.');
    } finally {
      setIsLoading(false);
    }
  };

  const handleSkipCrawl = () => {
    setIsCrawlPlaying(false);
    setCrawlText([]);
    setIsLoading(false);
  };

  // Handle icon rotation on click
  const handleIconClick = (fetchFunction: () => void, iconKey: string) => {
    fetchFunction();
    setRotatedIcons((prev) => ({ ...prev, [iconKey]: !prev[iconKey] }));
  };

  // Conditionally render mobile view or desktop view
  return (
    <div className="App">
      {/* Loading Screen */}
      {isLoading && !isCrawlPlaying && (
        <div className="loading-screen">
          <p>Loading your Star Wars adventure...</p>
        </div>
      )}

      {/* Star Wars Crawl */}
      {isCrawlPlaying && (
        <StarWarsCrawl text={crawlText} onSkip={handleSkipCrawl} duration={50} />
      )}

      {/* Main Content */}
      {!isCrawlPlaying && !isLoading && (
        <div className="main-content">
          {isMobile ? (
            <div className="mobile-view">
              <div className="container mt-4">
                <div className="row">
                  <div
                    className="col-4 text-center mb-4"
                    onClick={() => handleIconClick(fetchRandomPerson, 'person')}
                  >
                    <i
                      className={`bi bi-person-fill icon mb-2 ${
                        rotatedIcons['person'] ? 'rotated' : ''
                      }`}
                    ></i>
                    <p>{people ? people.name : 'Person'}</p>
                  </div>
                  <div
                    className="col-4 text-center mb-4"
                    onClick={() => handleIconClick(fetchRandomStarship, 'starship')}
                  >
                    <i
                      className={`bi bi-rocket-fill icon mb-2 ${
                        rotatedIcons['starship'] ? 'rotated' : ''
                      }`}
                    ></i>
                    <p>{starship ? starship.name : 'Starship'}</p>
                  </div>
                  <div
                    className="col-4 text-center mb-4"
                    onClick={() => handleIconClick(fetchRandomPlanet, 'planet')}
                  >
                    <i
                      className={`bi bi-globe icon mb-2 ${
                        rotatedIcons['planet'] ? 'rotated' : ''
                      }`}
                    ></i>
                    <p>{planet ? planet.name : 'Planet'}</p>
                  </div>
                </div>
                <div className="row">
                  <div
                    className="col-4 text-center mb-4"
                    onClick={() => handleIconClick(fetchRandomFilm, 'film')}
                  >
                    <i
                      className={`bi bi-film icon mb-2 ${
                        rotatedIcons['film'] ? 'rotated' : ''
                      }`}
                    ></i>
                    <p>{film ? film.title : 'Film'}</p>
                  </div>
                  <div
                    className="col-4 text-center mb-4"
                    onClick={() => handleIconClick(fetchRandomVehicle, 'vehicle')}
                  >
                    <i
                      className={`bi bi-truck icon mb-2 ${
                        rotatedIcons['vehicle'] ? 'rotated' : ''
                      }`}
                    ></i>
                    <p>{vehicle ? vehicle.name : 'Vehicle'}</p>
                  </div>
                  <div
                    className="col-4 text-center mb-4"
                    onClick={() => handleIconClick(fetchRandomSpecies, 'species')}
                  >
                    <i
                      className={`bi bi-bug-fill icon mb-2 ${
                        rotatedIcons['species'] ? 'rotated' : ''
                      }`}
                    ></i>
                    <p>{species ? species.name : 'Species'}</p>
                  </div>
                </div>
              </div>
              {/* GO Button */}
              <div className="go-button-container">
                <button
                  className="btn btn-success w-100"
                  onClick={handleGoClick}
                  disabled={isLoading || isCrawlPlaying}
                >
                  {isLoading ? 'Loading...' : 'GO'}
                </button>
              </div>
            </div>
          ) : (
            <div className="desktop-view">
              {/* Navbar Section */}
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
                          <i className="bi bi-person-fill"></i> {people ? people.name : 'Person'}
                        </a>
                      </li>
                      <li
                        className="nav-item"
                        onClick={() => handleIconClick(fetchRandomStarship, 'starship')}
                      >
                        <a className="nav-link" href="#">
                          <i className="bi bi-rocket-fill"></i> {starship ? starship.name : 'Starship'}
                        </a>
                      </li>
                      <li
                        className="nav-item"
                        onClick={() => handleIconClick(fetchRandomPlanet, 'planet')}
                      >
                        <a className="nav-link" href="#">
                          <i className="bi bi-globe"></i> {planet ? planet.name : 'Planet'}
                        </a>
                      </li>
                      <li
                        className="nav-item"
                        onClick={() => handleIconClick(fetchRandomFilm, 'film')}
                      >
                        <a className="nav-link" href="#">
                          <i className="bi bi-film"></i> {film ? film.title : 'Film'}
                        </a>
                      </li>
                      <li
                        className="nav-item"
                        onClick={() => handleIconClick(fetchRandomVehicle, 'vehicle')}
                      >
                        <a className="nav-link" href="#">
                          <i className="bi bi-truck"></i> {vehicle ? vehicle.name : 'Vehicle'}
                        </a>
                      </li>
                      <li
                        className="nav-item"
                        onClick={() => handleIconClick(fetchRandomSpecies, 'species')}
                      >
                        <a className="nav-link" href="#">
                          <i className="bi bi-bug-fill"></i> {species ? species.name : 'Species'}
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

              {/* Main Content */}
              <div className="main-content">
                <div className="welcome-content">
                  <h1>Embark on a Galactic Adventure!</h1>
                  <p>Select your characters and click 'GO' to begin.</p>
                </div>
              </div>
            </div>
          )}
        </div>
      )}

      {/* Display Error Message */}
      {error && (
        <div
          className="alert alert-danger position-fixed bottom-0 start-50 translate-middle-x mb-3"
          role="alert"
        >
          {error}
        </div>
      )}
    </div>
  );
};

export default App;
