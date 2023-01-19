<template>
  <div class="menu-wrapper">
    <h2>Menu</h2>

    <div class="search">
      <input type="text" v-model.trim="search" />
    </div>

    <div class="loading" v-if="loading">loading</div>
    <div class="menu-items" v-if="!loading && products.length">
      <MenuItem
        v-for="product in filteredProducts"
        :key="product.id"
        :product="product"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: 'OrderMenu',
};
</script>

<script setup>
import { storeToRefs } from 'pinia';
import { useProductsStore } from '@/stores/products';
import MenuItem from '@/components/order/menu/MenuItem.vue';
import { computed, ref } from 'vue';

const { fetchProducts } = useProductsStore();
fetchProducts();

const { products, loading } = storeToRefs(useProductsStore());
const search = ref('');
const filteredProducts = computed(() => {
  const lcSearch = search.value.toLowerCase();
  return products.value.filter((p) => p.name.toLowerCase().includes(lcSearch));
});
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.menu-wrapper {
  max-height: 100%;
  display: flex;
  flex-direction: column;
}

h2 {
  @include styled-h2;
}

.menu-items {
  flex-grow: 1;
  max-height: 100%;
  overflow: auto;
}
</style>
