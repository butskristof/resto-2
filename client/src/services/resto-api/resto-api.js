import axios from 'axios';
import { RESTO_API_BASE_URL } from '@/utilities/env';

export default axios.create({
  baseURL: RESTO_API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});
