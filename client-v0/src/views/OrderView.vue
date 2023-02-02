<template>
  <div class="order-container">
    <OrderMenu />
    <CurrentOrder />
  </div>
</template>

<script setup>
import OrderMenu from '@/components/order/OrderMenu.vue';
import CurrentOrder from '@/components/order/CurrentOrder.vue';
</script>

<style scoped lang="scss">
.order-container {
  display: flex;
  flex-direction: row;
  gap: 1rem;
  height: 100%;

  > * {
    flex-basis: 50%;
  }
}
</style>
