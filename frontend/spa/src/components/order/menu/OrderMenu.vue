<template>
  <div class="menu">
    <LoadingIndicator v-if="productsLoading" class="loading-indicator">
      Gerechten laden
    </LoadingIndicator>

    <template v-else>
      <div class="search">
        <TextInput v-model.trim="search" placeholder="Zoek gerecht" />
      </div>

      <div class="products">
        <div v-if="productsQueryFailed">
          <div>
            Er liep iets mis bij het ophalen van de gerechten, probeer het later
            opnieuw.
          </div>
          <div>
            <pre>{{ productsQueryError }}</pre>
          </div>
        </div>

        <template v-if="productsQuerySuccess">
          <ProductListItem
            v-for="product in filteredProducts"
            :key="product.id"
            :product="product"
            :order-key="orderKey"
            @add="addToCurrentOrder"
          />
        </template>
      </div>
    </template>
  </div>
</template>

<script setup>
import { computed, ref } from 'vue';
import { useProductsQuery } from '@/composables/queries';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import ProductListItem from '@/components/order/menu/ProductListItem.vue';
import { useCurrentOrderStore } from '@/stores/current-order';
import TextInput from '@/components/common/form/inputs/TextInput.vue';
import { storeToRefs } from 'pinia';

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
const { add: addToCurrentOrder } = useCurrentOrderStore();
const { key: orderKey } = storeToRefs(useCurrentOrderStore());
</script>

<style scoped lang="scss">
@import '@/styles/utilities/_padding-margin.scss';

.menu {
  max-height: 100%;

  display: flex;
  flex-direction: column;
  gap: $box-padding;

  .loading-indicator {
    display: flex;
    justify-content: center;
    padding: $box-padding;
  }

  .search {
    flex-shrink: 0;
    padding: $box-padding;
  }

  .products {
    max-height: 100%;
    overflow: auto;
  }
}
</style>
