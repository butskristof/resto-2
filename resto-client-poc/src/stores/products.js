import { defineStore } from 'pinia';
import { ref } from 'vue';
import { getProducts } from '@/services/products.service';

export const useProductsStore = defineStore('products', () => {
  const products = ref([]);
  const loading = ref(false);
  function fetchProducts() {
    console.log('fetch products');
    loading.value = true;
    products.value = getProducts();
    loading.value = false;
  }

  return { products, fetchProducts };
});
