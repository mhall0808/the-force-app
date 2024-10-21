// src/components/EntitySelector.tsx

interface EntitySelectorProps {
  entities: any;
  rotatedIcons: { [key: string]: boolean };
  handleIconClick: (fetchFunction: () => void, iconKey: string) => void;
  isMobile: boolean;
}

const entityKeys = ['person', 'starship', 'planet', 'film', 'vehicle', 'species'] as const;
type EntityKey = typeof entityKeys[number]; // 'person' | 'starship' | 'planet' | 'film' | 'vehicle' | 'species'

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
}) => {
  return (
    <div className={isMobile ? 'row' : 'd-flex'}>
      {entityKeys.map((key) => (
        <div
          key={key}
          className={isMobile ? 'col-4 text-center mb-4' : 'me-3'}
          onClick={() => handleIconClick(() => {}, key)}
        >
          <i
            className={`${iconClasses[key]} icon mb-2 ${rotatedIcons[key] ? 'rotated' : ''}`}
          ></i>
          <p>{entities[key] ? entities[key].name || entities[key].title : key.charAt(0).toUpperCase() + key.slice(1)}</p>
        </div>
      ))}
    </div>
  );
};

export default EntitySelector;
