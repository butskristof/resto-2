import { createAxiosInstance } from '@/services/resto-api/base-api.js';

const restoApi = createAxiosInstance('/api');

class CategoriesService {
  async get(page = 1, pageSize = 10) {
    // await new Promise((r) => setTimeout(r, 1000));
    return await restoApi.get(`/categories?page=${page}&pageSize=${pageSize}`);
  }

  async getById(id) {
    return await restoApi.get(`/categories/${id}`);
  }

  create(data) {
    return restoApi.post('/categories', data);
  }

  update(id, data) {
    return restoApi.put(`/categories/${id}`, data);
  }

  delete(id) {
    return restoApi.delete(`/categories/${id}`);
  }
}

export default new CategoriesService();
