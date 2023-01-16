<template>
  <h2>Order</h2>
  <div class="order-container">
    <div class="order-menu">
      <OrderMenu />
    </div>
    <div class="current-order">
      <CurrentOrder />
    </div>
  </div>
</template>

<script>
import OrderMenu from '@/components/order/menu/OrderMenu.vue';
import CurrentOrder from '@/components/order/CurrentOrder.vue';

export default {
  name: 'OrderView',
  components: { CurrentOrder, OrderMenu },
};
</script>

<style scoped lang="scss">
.order-container {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: 1rem;

  .order-menu {
    flex-grow: 1;
  }

  .current-order {
    flex-basis: 30%;
    min-width: 350px;
    border: 1px solid black;
  }
}
</style>
