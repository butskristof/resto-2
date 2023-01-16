import { defineStore } from 'pinia';
import { ref } from 'vue';
import { getProducts } from '@/services/products.service';

export const useProductsStore = defineStore('products', () => {
  const products = ref([]);
  const loading = ref(false);
  function fetchProducts() {
    loading.value = true;
    try {
      products.value = getProducts();
    } finally {
      loading.value = false;
    }
  }

  return { products, loading, fetchProducts };
});
