<template>
  <h3>Menu</h3>
  <MenuItem v-for="product in products" :key="product.id" :product="product" />
</template>

<script setup>
import MenuItem from '@/components/order/menu/MenuItem.vue';
import { useProductsStore } from '@/stores/products';
import { storeToRefs } from 'pinia';

const { products } = storeToRefs(useProductsStore());
const { fetchProducts } = useProductsStore();

fetchProducts();
</script>

<style scoped></style>
