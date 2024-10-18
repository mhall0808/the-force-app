import React from 'react';
import { useStarshipData } from '../../hooks/useStarshipData'; // Path to your hook

const StarshipList: React.FC = () => {
  const { starships, loading, error } = useStarshipData(); // Use the custom hook

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {(error as any).message}</div>;
  }

  return (
    <div>
      <h2>Starship List</h2>
      <ul>
        {starships.map((starship: any) => (
          <li key={starship.id}>
            {starship.name} - {starship.model}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default StarshipList;
