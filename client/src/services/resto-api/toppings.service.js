import restoApi from '@/services/resto-api/resto-api';

class ToppingsService {
  async get(page = 1, pageSize = 10) {
    await new Promise((r) => setTimeout(r, 1000));
    return await restoApi.get(`/toppings?page=${page}&pageSize=${pageSize}`);
  }

  create(data) {
    return restoApi.post('/toppings', data);
  }

  update(id, data) {
    return restoApi.put(`/toppings/${id}`, data);
  }

  delete(id) {
    return restoApi.delete(`/toppings/${id}`);
  }
}
export default new ToppingsService();
