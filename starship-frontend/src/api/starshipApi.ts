// starshipApi.ts

import axios from 'axios';

// Define the Starship interface here (or import it if you already have it elsewhere)
interface Starship {
  id: number;
  name: string;
  model: string;
  [key: string]: any;  // Any other potential properties from the API
}

// Fetch all starships with a typed return value
export const fetchAllStarships = async (): Promise<Starship[]> => {
  const response = await axios.get<Starship[]>('http://localhost:5000/api/starships'); // Adjust the API URL as needed
  return response.data;
};
