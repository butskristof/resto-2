<template>
  <div class="header">
    <div class="left">
      <h2>Bestellingshistoriek</h2>
    </div>
    <div class="right">
      <LoadingIndicator v-if="loading">{{ loadingLabel }}</LoadingIndicator>
    </div>
  </div>

  <div v-if="isError">
    <div>
      Er liep iets mis bij het ophalen van de bestellingen, probeer het later
      opnieuw.
    </div>
    <div>
      <pre>{{ error }}</pre>
    </div>
  </div>

  <div v-if="isSuccess">
    <div class="orders">
      <OrderHistoryListItem
        v-for="order in orders"
        :key="order.id"
        :order="order"
      />
    </div>

    <LoadNextPage
      v-if="hasNextPage"
      entity="bestellingen"
      @load-next-page="fetchNextPage"
    />
  </div>
</template>

<script setup>
import { useOrdersQuery } from '@/composables/queries';
import { computed } from 'vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import LoadNextPage from '@/components/common/LoadNextPage.vue';
import OrderHistoryListItem from '@/components/order-history/OrderHistoryListItem.vue';

const {
  orders,
  fetchNextPage,
  hasNextPage,
  isLoading,
  isFetching,
  isFetchingNextPage,
  isSuccess,
  isError,
  error,
} = useOrdersQuery();
const loading = computed(
  () => isLoading.value || isFetching.value || isFetchingNextPage.value,
);
const loadingLabel = computed(() => {
  if (isLoading.value || isFetchingNextPage.value) return 'Bestellingen laden';
  else if (isFetching.value) return 'Bestellingen bijwerken';
  return '';
});
</script>

<style scoped lang="scss">
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/ui/_layout.scss';

.header {
  @include layout.flex-row-space-between;
}

h2 {
  margin: auto auto padding-margin.$box-padding;
}
</style>
