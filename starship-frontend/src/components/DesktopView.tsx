import React from 'react';
import Navbar from './Navbar';

interface DesktopViewProps {
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

const DesktopView: React.FC<DesktopViewProps> = ({
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
  <div className="desktop-view">
    {/* Navbar Section */}
    <Navbar
      entities={entities}
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

    {/* Main Content */}
    <div className="main-content">
      <div className="welcome-content">
        <h1>Embark on a Galactic Adventure!</h1>
        <p>Select your characters and click 'GO' to begin.</p>
      </div>
    </div>
  </div>
);

export default DesktopView;
