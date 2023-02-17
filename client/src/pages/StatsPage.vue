<template>
  <div class="stats-page">
    <pre>
      {{ JSON.stringify(stats, null, 2) }}
    </pre>
  </div>
</template>

<script setup>
import OrdersService from '@/services/resto-api/orders.service';

const stats = await OrdersService.getStats();
</script>

<style scoped lang="scss">
@import '@/styles/_colors.scss';

.stats-page {
  //background-color: $klj-red;
}
</style>
