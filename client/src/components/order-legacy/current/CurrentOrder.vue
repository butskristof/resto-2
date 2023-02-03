<template>
  <CurrentOrderTicket />
  <div>Total: {{ currentOrderTotal }}</div>
  <label>
    Cash received:
    <input v-model.number="cashReceived" type="number" />
  </label>
  <div>To return: {{ cashToReturn }}</div>
  <div>
    <button type="button">order</button>
    <button type="button" @click="reset">reset</button>
  </div>
</template>

<script>
export default {
  name: 'CurrentOrder',
};
</script>

<script setup>
import CurrentOrderTicket from '@/components/order-legacy/current/CurrentOrderTicket.vue';
import { storeToRefs } from 'pinia';
import { useCurrentOrderStore } from '@/stores/current-order';
import { computed, ref } from 'vue';

const { currentOrderTotal } = storeToRefs(useCurrentOrderStore());
const cashReceived = ref(0);
const cashToReturn = computed(
  () => cashReceived.value - currentOrderTotal.value,
);

function reset() {
  resetOrder();
  cashReceived.value = 0;
}

const { reset: resetOrder } = useCurrentOrderStore();
</script>

<style scoped></style>
