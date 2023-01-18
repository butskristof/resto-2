import { createRouter, createWebHistory } from 'vue-router';
import OrderPage from '@/pages/OrderPage.vue';

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
      component: OrderPage,
    },
    {
      path: '/manage',
      name: 'Manage',
      redirect: { name: 'ManageProducts' },
      component: () => import('@/pages/manage/ManagePage.vue'),
      children: [
        {
          path: 'products',
          name: 'ManageProducts',
          component: () => import('@/pages/manage/ManageProductsPage.vue'),
        },
        {
          path: 'toppings',
          name: 'ManageToppings',
          component: () => import('@/pages/manage/ManageToppingsPage.vue'),
        },
      ],
    },
  ],
});

export default router;