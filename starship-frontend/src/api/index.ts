import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5000', // Set your backend URL here
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;
