import { useEffect, useState } from 'react';
import { fetchAllStarships } from '../api/starshipApi';  // Correct import path

interface Starship {
  id: number;
  name: string;
  model: string;
  [key: string]: any;
}

export const useStarshipData = () => {
  const [starships, setStarships] = useState<Starship[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    const getStarships = async () => {
      try {
        const data = await fetchAllStarships();  // Fetch starships, properly typed
        setStarships(data);  // Now this will be correctly typed
      } catch (err: unknown) {
        if (err instanceof Error) {
          setError(err);
        }
      } finally {
        setLoading(false);
      }
    };

    getStarships();
  }, []);

  return { starships, loading, error };
};
