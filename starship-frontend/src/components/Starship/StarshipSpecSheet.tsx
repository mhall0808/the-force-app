import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Spinner } from 'react-bootstrap';

interface Starship {
  name: string;
  manufacturer: string;
  length: string;
  crew: string;
  passengers: string;
  cargo_capacity: string;
}

const StarshipSpecSheet: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [starship, setStarship] = useState<Starship | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Simulate API call with setTimeout
    setTimeout(() => {
      // Mock data for a starship, you will replace this with actual API call
      const fetchedStarship = {
        name: "CR90 corvette",
        manufacturer: "Corellian Engineering Corporation",
        length: "150 meters",
        crew: "30-165",
        passengers: "600",
        cargo_capacity: "3000000",
      };
      setStarship(fetchedStarship);
      setLoading(false);
    }, 1000); // Simulate delay
  }, [id]);

  if (loading) {
    return <Spinner animation="border" />;
  }

  return (
    <div className="container mt-4">
      <h1>{starship?.name}</h1>
      <h3>By {starship?.manufacturer}</h3>
      <ul>
        <li>Length: {starship?.length}</li>
        <li>Crew: {starship?.crew}</li>
        <li>Passengers: {starship?.passengers}</li>
        <li>Cargo Capacity: {starship?.cargo_capacity}</li>
      </ul>
    </div>
  );
};

export default StarshipSpecSheet;
