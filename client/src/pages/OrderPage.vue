<template>
  <div class="order">
    <OrderMenu class="menu" />
    <div class="current-order">current</div>
  </div>
</template>

<script setup>
import OrderMenu from '@/components/order/menu/OrderMenu.vue';
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.order {
  display: flex;
  flex-direction: row;
  //flex-wrap: wrap;
  gap: $box-padding;

  max-height: 100%;

  .menu,
  .current-order {
    flex-grow: 1;
    flex-basis: 50%;
  }
}
</style>
