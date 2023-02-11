<template>
  <div class="order-page">
    <OrderMenu class="menu" />
    <CurrentOrder class="current-order" />
  </div>
</template>

<script setup>
import OrderMenu from '@/components/order/menu/OrderMenu.vue';
import CurrentOrder from '@/components/order/current-order/CurrentOrder.vue';
</script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_responsiveness.scss';
@import '@/styles/utilities/_padding-margin.scss';

.order-page {
  max-height: 100%;

  @include flex-row;
  gap: $box-padding;

  // only wrap below lg breakpoint
  // otherwise, distribute evenly w/ flex-basis 50%
  flex-wrap: wrap;
  @include breakpoint-lg-up {
    flex-wrap: initial;
  }

  .menu,
  .current-order {
    flex-grow: 1;
    flex-shrink: 0;
    flex-basis: 50%;
    overflow: auto;
  }
}
</style>
