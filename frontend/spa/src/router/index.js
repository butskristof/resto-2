import { createRouter, createWebHistory } from 'vue-router';
import routeInfo from '@/router/route-info';
import OrderPage from '@/pages/OrderPage.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: { name: routeInfo.order.name },
    },
    {
      name: routeInfo.order.name,
      path: routeInfo.order.path,
      component: OrderPage,
    },
    {
      name: routeInfo.orderHistory.name,
      path: routeInfo.orderHistory.path,
      component: () => import('@/pages/OrderHistoryPage.vue'),
    },
    {
      path: routeInfo.manage.path,
      name: routeInfo.manage.name,
      redirect: { name: routeInfo.manage.products.name },
      component: () => import('@/pages/manage/ManagePage.vue'),
      children: [
        {
          path: routeInfo.manage.products.path,
          name: routeInfo.manage.products.name,
          component: () => import('@/pages/manage/ManageProductsPage.vue'),
        },
        {
          path: routeInfo.manage.toppings.path,
          name: routeInfo.manage.toppings.name,
          component: () => import('@/pages/manage/ManageToppingsPage.vue'),
        },
        {
          path: routeInfo.manage.categories.path,
          name: routeInfo.manage.categories.name,
          component: () => import('@/pages/manage/ManageCategoriesPage.vue'),
        },
      ],
    },
    {
      name: routeInfo.stats.name,
      path: routeInfo.stats.path,
      component: () => import('@/pages/StatsPage.vue'),
    },
  ],
});

export default router;
