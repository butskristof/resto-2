<template>
  <div class="stats-page">
    <LoadingIndicator v-if="loading">Statistieken laden</LoadingIndicator>
    <div v-if="isError" class="error">{{ error }}</div>
    <pre v-if="isSuccess">{{ JSON.stringify(data, null, 2) }}</pre>
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
