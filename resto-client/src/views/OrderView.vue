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

<script>
import OrderMenu from '@/components/order/menu/OrderMenu.vue';
import CurrentOrder from '@/components/order/current/CurrentOrder.vue';

export default {
  name: 'OrderView',
  components: { CurrentOrder, OrderMenu },
};
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
