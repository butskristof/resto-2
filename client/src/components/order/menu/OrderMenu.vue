<template>
  <div class="menu">
    <div class="search">
      <input v-model.trim="search" type="text" placeholder="Zoek gerecht" />
    </div>

    <div class="products">
      <LoadingIndicator v-if="productsLoading">
        Gerechten laden
      </LoadingIndicator>

      <div v-if="productsQueryFailed">
        <div>
          Er liep iets mis bij het ophalen van de categorieën, probeer het later
          opnieuw.
        </div>
        <div>
          <pre>{{ productsQueryError }}</pre>
        </div>
      </div>

      <template v-if="productsQuerySuccess">
        <ProductMenuItem
          v-for="product in filteredProducts"
          :key="product.id"
          :product="product"
        />
      </template>
    </div>
  </div>
</template>

<script setup>
import { computed, ref } from 'vue';
import ProductMenuItem from '@/components/order/menu/ProductMenuItem.vue';
import { useProductsQuery } from '@/composables/queries';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';

const search = ref('');
const {
  products,
  isLoading: productsLoading,
  isError: productsQueryFailed,
  error: productsQueryError,
  isSuccess: productsQuerySuccess,
} = useProductsQuery(true);
const filteredProducts = computed(() => {
  const lcSearch = search.value.toLowerCase();
  return products.value.filter((p) => p.name.toLowerCase().includes(lcSearch));
});
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.search {
  padding: 0 $box-padding;
  margin-bottom: 1rem;

  input {
    //margin: $box-padding;
    width: 100%;
  }
}

.menu {
  max-height: 100%;
  display: flex;
  flex-direction: column;

  .products {
    max-height: 100%;
    overflow: auto;
  }
}
</style>
