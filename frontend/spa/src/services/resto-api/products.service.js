import restoApi from '@/services/resto-api/resto-api';

class ProductsService {
  async get(page = 1, pageSize = 10) {
    // await new Promise((r) => setTimeout(r, 1000));
    return await restoApi.get(`/products?page=${page}&pageSize=${pageSize}`);
  }

  create(data) {
    return restoApi.post('/products', data);
  }

  update(id, data) {
    return restoApi.put(`/products/${id}`, data);
  }

  delete(id) {
    return restoApi.delete(`/products/${id}`);
  }
}

export default new ProductsService();
