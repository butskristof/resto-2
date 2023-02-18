<template>
  <div class="header">
    <div class="left">
      <h2>Statistieken</h2>
    </div>
    <div class="right">
      <LoadingIndicator v-if="loading">Statistieken laden</LoadingIndicator>
    </div>
  </div>
  <div v-if="isError" class="error">{{ error }}</div>
  <div v-if="isSuccess">
    <div class="order-count">Aantal bestellingen: {{ data.orderCount }}</div>
    <OrderStatsLineChart />
    <ProductStatsBarChart />
  </div>
</template>

<script setup>
import { useOrderStatisticsQuery } from '@/composables/queries';
import { computed } from 'vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import ProductStatsBarChart from '@/components/order-stats/charts/ProductStatsBarChart.vue';
import OrderStatsLineChart from '@/components/order-stats/charts/OrderStatsLineChart.vue';

const { data, isLoading, isFetching, isSuccess, isError, error } =
  useOrderStatisticsQuery();
const loading = computed(() => isLoading.value || isFetching.value);
</script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_padding-margin.scss';

.header {
  @include flex-row-space-between;
  align-items: center;
}

.order-count {
  margin-bottom: $box-padding;
}
</style>
