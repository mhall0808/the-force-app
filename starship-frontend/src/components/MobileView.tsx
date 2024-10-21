// MobileView.tsx

import React from 'react';
import EntitySelector from './EntitySelector';

interface MobileViewProps {
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

const MobileView: React.FC<MobileViewProps> = ({
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
}) => {
  // Create a mapping of fetch functions
  const fetchFunctions = {
    person: fetchRandomPerson,
    starship: fetchRandomStarship,
    planet: fetchRandomPlanet,
    film: fetchRandomFilm,
    vehicle: fetchRandomVehicle,
    species: fetchRandomSpecies,
  };

  return (
    <div className="mobile-view">
      <div className="container mt-4">
        <EntitySelector
          entities={entities}
          rotatedIcons={rotatedIcons}
          handleIconClick={handleIconClick}
          isMobile={true}
          fetchFunctions={fetchFunctions} // Pass fetch functions to EntitySelector
        />
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
  );
};

export default MobileView;
