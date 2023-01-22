import restoApi from '@/services/resto-api/resto-api';

class CategoriesService {
  get(page = 1, pageSize = 10) {
    return restoApi.get(`/categories?page=${page}&pageSize=${pageSize}`);
  }

  getById(id) {
    return restoApi.get(`/categories/${id}`);
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
