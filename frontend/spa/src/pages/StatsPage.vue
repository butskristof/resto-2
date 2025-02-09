<template>
  <div class="stats-page">
    <OrderStatistics />
  </div>
</template>

<script>
import { defineComponent } from 'vue';
import OrderStatistics from '@/components/order-stats/OrderStatistics.vue';

export default defineComponent({
  components: { OrderStatistics },
});
</script>

<style scoped lang="scss">
@use '@/styles/_colors.scss';

.stats-page {
  //background-color: colors.$klj-red;
}
</style>
