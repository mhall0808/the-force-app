import { useEffect, useState } from 'react';
import { fetchAllEntities } from '../api/starshipApi';  // Adjusted import for the generic API file

interface Entity {
  id: number;
  name: string;
  [key: string]: any;
}

export const useEntityData = <T extends Entity>(entityType: string) => {
  const [entities, setEntities] = useState<T[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    const getEntities = async () => {
      try {
        const data = await fetchAllEntities(entityType);  // Fetch entities based on the type
        setEntities(data as T[]);  // Type assertion to ensure TypeScript knows the type
      } catch (err: unknown) {
        if (err instanceof Error) {
          setError(err);
        }
      } finally {
        setLoading(false);
      }
    };

    getEntities();
  }, [entityType]);

  return { entities, loading, error };
};
