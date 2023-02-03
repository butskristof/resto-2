import { useQuery } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import ToppingsService from '@/services/resto-api/toppings.service';
import CategoriesService from '@/services/resto-api/categories.service';
import ProductsService from '@/services/resto-api/products.service';

// TODO add property that extracts .results

export function useToppingsQuery() {
  return useQuery({
    queryKey: QUERY_KEYS.TOPPINGS,
    queryFn: async () => (await ToppingsService.get()).data,
  });
}

export function useCategoriesQuery() {
  return useQuery({
    queryKey: QUERY_KEYS.CATEGORIES,
    queryFn: async () => (await CategoriesService.get()).data,
  });
}

export function useProductsQuery() {
  return useQuery({
    queryKey: QUERY_KEYS.PRODUCTS,
    queryFn: async () => (await ProductsService.get()).data,
  });
}
