<template>
  <div class="ticket">
    <div class="current-order-item" v-for="(item, i) in currentOrder" :key="i">
      <div class="description">{{ item.description }}</div>
      <div class="amount">
        <button type="button" @click="increment(i)">+</button>
        <button type="button" @click="decrement(i)">-</button>
        {{ item.count }} x&nbsp;
      </div>
      <div class="unit-price">{{ item.unitPrice }}</div>
      &nbsp;=&nbsp;
      <div class="total-price">{{ item.totalPrice }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CurrentOrderTicket',
};
</script>

<script setup>
import { useCurrentOrderStore } from '@/stores/current-order';
import { storeToRefs } from 'pinia';

const { extendedCurrentOrder: currentOrder } = storeToRefs(
  useCurrentOrderStore(),
);
const { increment, decrement } = useCurrentOrderStore();
</script>

<style scoped lang="scss">
.ticket {
  border: 1px solid black;
  font-family: monospace;
}

.current-order-item {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  align-items: center;

  border-bottom: 1px solid black;

  .description {
    flex-basis: 100%;
  }
}
</style>
