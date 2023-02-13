import restoApi from '@/services/resto-api/resto-api';

class OrdersService {
  async get(page = 1, pageSize = 1) {
    // await new Promise((r) => setTimeout(r, 1000));
    return await restoApi.get(`/orders?page=${page}&pageSize=${pageSize}`);
  }

  create(data) {
    return restoApi.post('/orders', data);
  }

  print(orderId) {
    return restoApi.post(`/orders/${orderId}/print`, { orderId });
  }
}
export default new OrdersService();
