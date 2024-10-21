import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import './App.css'; // Your custom styles
import axios from 'axios';
import StarWarsCrawl from './components/StarWarsCrawl';
import LoadingScreen from './components/LoadingScreen';
import ErrorMessage from './components/ErrorMessage';
import MobileView from './components/MobileView';
import DesktopView from './components/DesktopView';

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

  interface ParagraphResponse {
    paragraphText: string;
  }

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
    const sanitizedData = sanitizeSelectedData(selectedData);
  
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
      const response = await axios.post<ParagraphResponse>('http://localhost:5000/api/ai/generate-crawl-paragraph', {
        selectedData,
        paragraphNumber: 3,
      });
  
      const paragraph3Text = (response.data as ParagraphResponse).paragraphText; // Cast response.data to ParagraphResponse
  
      // Once all paragraphs are ready, set the crawl text and start the crawl
      const crawlContent = [
        film ? film.title.toUpperCase() : 'STAR WARS', // Film title
        paragraph1,
        paragraph2,
        paragraph3Text,
        paragraph4,
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

  // Conditionally render mobile or desktop view
  return (
    <div className="App">
      {isLoading && !isCrawlPlaying && <LoadingScreen />}
      {isCrawlPlaying && <StarWarsCrawl text={crawlText} onSkip={handleSkipCrawl} duration={50} />}
      {!isCrawlPlaying && !isLoading && (
        <div className="main-content">
          {isMobile ? (
            <MobileView
            entities={{ person: people, starship, planet, film, vehicle, species }}
            rotatedIcons={rotatedIcons}
            handleIconClick={handleIconClick}
            handleGoClick={handleGoClick}
            isLoading={isLoading}
            isCrawlPlaying={isCrawlPlaying}
            fetchRandomPerson={fetchRandomPerson}
            fetchRandomStarship={fetchRandomStarship}
            fetchRandomPlanet={fetchRandomPlanet}
            fetchRandomFilm={fetchRandomFilm}
            fetchRandomVehicle={fetchRandomVehicle}
            fetchRandomSpecies={fetchRandomSpecies}
          />
          ) : (
            <DesktopView
              entities={{ people, starship, planet, film, vehicle, species }}
              rotatedIcons={rotatedIcons}
              handleIconClick={handleIconClick}
              handleGoClick={handleGoClick}
              isLoading={isLoading}
              isCrawlPlaying={isCrawlPlaying}
              fetchRandomPerson={fetchRandomPerson}
              fetchRandomStarship={fetchRandomStarship}
              fetchRandomPlanet={fetchRandomPlanet}
              fetchRandomFilm={fetchRandomFilm}
              fetchRandomVehicle={fetchRandomVehicle}
              fetchRandomSpecies={fetchRandomSpecies}
            />
          )}
        </div>
      )}
      {error && <ErrorMessage message={error} />}
    </div>
  );
};

export default App;
