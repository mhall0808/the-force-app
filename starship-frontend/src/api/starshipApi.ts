import axios from 'axios';

export const fetchAllEntities = async (entityType: string) => {
  const response = await axios.get(`http://localhost:5000/api/${entityType}`);
  return response.data;
};

export const fetchRandomEntity = async (entityType: string) => {
  const response = await axios.get(`http://localhost:5000/api/random/${entityType}`);
  return response.data;
};
