<template>
  <div class="order">
    {{ order.id }} - {{ order.timestamp }} -
    <button type="button" @click="print">
      <i class="icon-printer"></i>
      Print
    </button>
  </div>
</template>

<script setup>
import OrdersService from '@/services/resto-api/orders.service';

const props = defineProps({
  order: {
    type: Object,
    required: true,
  },
});
function print() {
  return OrdersService.print(props.order.id);
}
</script>

<style scoped lang="scss">
@import '@/styles/utilities/_padding-margin.scss';

.order {
  margin-bottom: $box-padding;
}
</style>
