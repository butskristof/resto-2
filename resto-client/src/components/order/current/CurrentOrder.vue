<template>
  <CurrentOrderTicket />
  <div>Total: {{ currentOrderTotal }}</div>
  <label>
    Cash received:
    <input type="number" v-model.number="cashReceived" />
  </label>
  <div>To return: {{ cashToReturn }}</div>
  <div><button type="button">order</button></div>
</template>

<script setup>
import CurrentOrderTicket from '@/components/order/current/CurrentOrderTicket.vue';
import { storeToRefs } from 'pinia';
import { useCurrentOrderStore } from '@/stores/current-order';
import { computed, ref } from 'vue';

const { currentOrderTotal } = storeToRefs(useCurrentOrderStore());
const cashReceived = ref(0);
const cashToReturn = computed(
  () => cashReceived.value - currentOrderTotal.value,
);
</script>

<style scoped></style>
