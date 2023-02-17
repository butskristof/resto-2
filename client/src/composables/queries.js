import { useInfiniteQuery, useQuery } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import ToppingsService from '@/services/resto-api/toppings.service';
import CategoriesService from '@/services/resto-api/categories.service';
import ProductsService from '@/services/resto-api/products.service';
import { computed, watchEffect } from 'vue';
import OrdersService from '@/services/resto-api/orders.service';

//#region utilities

const getNextPageParam = (lastPage) =>
  lastPage.hasNextPage ? lastPage.currentPage + 1 : null;

const getAllResults = (query) =>
  query.data.value?.pages?.flatMap((p) => p.results) ?? [];

function fetchAllPagesWatcher(query) {
  const loading =
    query.isLoading.value ||
    query.isFetching.value ||
    query.isFetchingNextPage.value;
  if (!loading && query.isSuccess.value && query.hasNextPage.value)
    query.fetchNextPage();
}

//#endregion

export function useCategoriesQuery(fetchAllPages = false) {
  const query = useInfiniteQuery({
    queryKey: QUERY_KEYS.CATEGORIES,
    queryFn: async ({ pageParam = 1 }) =>
      (await CategoriesService.get(pageParam)).data,
    getNextPageParam,
  });

  const categories = computed(() => getAllResults(query));

  if (fetchAllPages) {
    watchEffect(() => fetchAllPagesWatcher(query));
  }

  return {
    ...query,
    categories,
  };
}

export function useToppingsQuery(fetchAllPages = false) {
  const query = useInfiniteQuery({
    queryKey: QUERY_KEYS.TOPPINGS,
    queryFn: async ({ pageParam = 1 }) =>
      (await ToppingsService.get(pageParam)).data,
    getNextPageParam,
  });

  const toppings = computed(() => getAllResults(query));

  if (fetchAllPages) {
    watchEffect(() => fetchAllPagesWatcher(query));
  }

  return {
    ...query,
    toppings,
  };
}

export function useProductsQuery(fetchAllPages = false) {
  const query = useInfiniteQuery({
    queryKey: QUERY_KEYS.PRODUCTS,
    queryFn: async ({ pageParam = 1 }) =>
      (await ProductsService.get(pageParam)).data,
    getNextPageParam,
  });

  const products = computed(() => getAllResults(query));

  if (fetchAllPages) {
    watchEffect(() => fetchAllPagesWatcher(query));
  }

  return {
    ...query,
    products,
  };
}

export function useOrdersQuery() {
  const query = useInfiniteQuery({
    queryKey: QUERY_KEYS.ORDERS,
    queryFn: async ({ pageParam = 1 }) =>
      (await OrdersService.get(pageParam)).data,
    getNextPageParam,
  });

  const orders = computed(() => getAllResults(query));

  return {
    ...query,
    orders,
  };
}

export function useOrderStatisticsQuery() {
  return useQuery({
    queryKey: QUERY_KEYS.ORDER_STATS,
    queryFn: async () => (await OrdersService.getStats()).data,
  });
}
