import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000/api';

export const fetchAllEntities = async (entityType: string) => {
  const response = await axios.get(`${API_BASE_URL}/${entityType}`);
  return response.data;
};

export const fetchEntityById = async (entityType: string, id: number) => {
  const response = await axios.get(`${API_BASE_URL}/${entityType}/${id}`);
  return response.data;
};

export const createEntity = async (entityType: string, data: any) => {
  const response = await axios.post(`${API_BASE_URL}/${entityType}`, data);
  return response.data;
};

export const updateEntity = async (entityType: string, id: number, data: any) => {
  const response = await axios.put(`${API_BASE_URL}/${entityType}/${id}`, data);
  return response.data;
};

export const deleteEntity = async (entityType: string, id: number) => {
  const response = await axios.delete(`${API_BASE_URL}/${entityType}/${id}`);
  return response.data;
};

export const fetchRandomEntity = async (entityType: string) => {
  const response = await axios.get(`${API_BASE_URL}/random/${entityType}`);
  return response.data;
};
