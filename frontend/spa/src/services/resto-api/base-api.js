import axios from 'axios';

export const createAxiosInstance = (baseURL) =>
  axios.create({
    baseURL,
    headers: {
      'x-csrf': 1,
    },
  });
