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
@import '@/styles/_colors.scss';

.stats-page {
  //background-color: $klj-red;
}
</style>
