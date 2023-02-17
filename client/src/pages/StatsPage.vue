<template>
  <div class="stats-page">
    <LoadingIndicator v-if="loading">Statistieken laden</LoadingIndicator>
    <div v-if="isError" class="error">{{ error }}</div>
    <div v-if="isSuccess">
      <ul>
        <li v-for="product in data.products" :key="product.id">
          {{ product.product.name }}: {{ product.quantity }}
          <ul v-if="product.toppings.length > 0">
            <li v-for="topping in product.toppings" :key="topping.topping.id">
              {{ topping.topping.name }}: {{ topping.quantity }}
            </li>
          </ul>
        </li>
      </ul>
    </div>
    <!--    <pre v-if="isSuccess">{{ JSON.stringify(data, null, 2) }}</pre>-->
  </div>
</template>

<script setup>
import { useOrderStatisticsQuery } from '@/composables/queries';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import { computed } from 'vue';

const { data, isLoading, isFetching, isSuccess, isError, error } =
  useOrderStatisticsQuery();
const loading = computed(() => isLoading.value || isFetching.value);
</script>

<style scoped lang="scss">
@import '@/styles/_colors.scss';

.stats-page {
  //background-color: $klj-red;
}
</style>
