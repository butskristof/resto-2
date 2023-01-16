import { createRouter, createWebHistory } from 'vue-router';
import OrderView from '@/views/OrderView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/order',
    },
    {
      path: '/order',
      name: 'Order',
      component: OrderView,
    },
  ],
});

export default router;
