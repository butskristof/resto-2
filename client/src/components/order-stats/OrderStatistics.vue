<template>
  <LoadingIndicator v-if="loading">Statistieken laden</LoadingIndicator>
  <div v-if="isError" class="error">{{ error }}</div>
  <div v-if="isSuccess">
    <!--    <ul>-->
    <!--      <li v-for="product in data.products" :key="product.id">-->
    <!--        {{ product.product.name }}: {{ product.quantity }}-->
    <!--        <ul v-if="product.toppings.length > 0">-->
    <!--          <li v-for="topping in product.toppings" :key="topping.topping.id">-->
    <!--            {{ topping.topping.name }}: {{ topping.quantity }}-->
    <!--          </li>-->
    <!--        </ul>-->
    <!--      </li>-->
    <!--    </ul>-->
    <ProductStatsBarChart />
  </div>
</template>

<script setup>
import { useOrderStatisticsQuery } from '@/composables/queries';
import { computed } from 'vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import ProductStatsBarChart from '@/components/order-stats/charts/ProductStatsBarChart.vue';

const { isLoading, isFetching, isSuccess, isError, error } =
  useOrderStatisticsQuery();
const loading = computed(() => isLoading.value || isFetching.value);
</script>

<style scoped></style>
