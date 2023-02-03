<template>
  <div class="order">
    <div class="menu">
      <OrderMenu />
    </div>
    <div class="current-order">
      <CurrentOrder />
    </div>
  </div>
</template>

<script setup>
import OrderMenu from '@/components/order-legacy/menu/OrderMenu.vue';
import CurrentOrder from '@/components/order-legacy/current/CurrentOrder.vue';
</script>

<style scoped lang="scss">
.order {
  display: flex;
  flex-direction: row;
  gap: 1rem;

  max-height: 100%;
  overflow: auto;
}

.menu,
.current-order {
  flex-grow: 1;
  flex-basis: 50%;
}
</style>
