import { useQuery } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import ToppingsService from '@/services/resto-api/toppings.service';
import CategoriesService from '@/services/resto-api/categories.service';
import ProductsService from '@/services/resto-api/products.service';
import { computed } from 'vue';

export function useCategoriesQuery() {
  const query = useQuery({
    queryKey: QUERY_KEYS.CATEGORIES,
    queryFn: async () => (await CategoriesService.get()).data,
  });

  const categories = computed(() => query.data.value?.results ?? []);

  return {
    ...query,
    categories,
  };
}

export function useToppingsQuery() {
  const query = useQuery({
    queryKey: QUERY_KEYS.TOPPINGS,
    queryFn: async () => (await ToppingsService.get()).data,
  });

  const toppings = computed(() => query.data.value?.results ?? []);

  return {
    ...query,
    toppings,
  };
}

export function useProductsQuery() {
  const query = useQuery({
    queryKey: QUERY_KEYS.PRODUCTS,
    queryFn: async () => (await ProductsService.get()).data,
  });

  const products = computed(() => query.data.value?.results ?? []);

  return {
    ...query,
    products,
  };
}
