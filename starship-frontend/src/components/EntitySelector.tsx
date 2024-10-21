// EntitySelector.tsx

import React from 'react';

interface EntitySelectorProps {
  entities: any;
  rotatedIcons: { [key: string]: boolean };
  handleIconClick: (fetchFunction: () => void, iconKey: string) => void;
  isMobile: boolean;
  fetchFunctions: {
    person: () => void;
    starship: () => void;
    planet: () => void;
    film: () => void;
    vehicle: () => void;
    species: () => void;
  };
}

const entityKeys = ['person', 'starship', 'planet', 'film', 'vehicle', 'species'] as const;
type EntityKey = typeof entityKeys[number];

const iconClasses: Record<EntityKey, string> = {
  person: 'bi bi-person-fill',
  starship: 'bi bi-rocket-fill',
  planet: 'bi bi-globe',
  film: 'bi bi-film',
  vehicle: 'bi bi-truck',
  species: 'bi bi-bug-fill',
};

const EntitySelector: React.FC<EntitySelectorProps> = ({
  entities,
  rotatedIcons,
  handleIconClick,
  isMobile,
  fetchFunctions,
}) => {
  return (
    <div className={isMobile ? 'row text-center' : 'd-flex'}>
      {entityKeys.map((key) => (
        <div
          key={key}
          className={isMobile ? 'col-4 mb-4' : 'me-3 text-center'}
          style={{ cursor: 'pointer' }}
          onClick={() => handleIconClick(fetchFunctions[key], key)}
        >
          {/* Icon with rotation effect */}
          <i
            className={`${iconClasses[key]} icon mb-2 ${rotatedIcons[key] ? 'rotated' : ''}`}
            style={{ fontSize: '2rem' }}
          ></i>
          {/* Entity Name */}
          <p>
            {entities[key]
              ? entities[key].name || entities[key].title
              : key.charAt(0).toUpperCase() + key.slice(1)}
          </p>
        </div>
      ))}
    </div>
  );
};

export default EntitySelector;
