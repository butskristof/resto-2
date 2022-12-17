<template>
  <div class="calculator">
    <div class="calculator-row">
      <div>Totaal bestelling</div>
      <div class="styled-amount order-total">
        {{ formatCurrency(orderTotal) }}
      </div>
    </div>
    <div class="calculator-row">
      <div>Cash ontvangen</div>
      <div>
        <input type="number" class="cash-received" v-model="cashReceived" />
      </div>
    </div>
    <div class="calculator-row">
      <div>Terug te geven</div>
      <div class="styled-amount cashback">{{ formatCurrency(cashback) }}</div>
    </div>
  </div>
</template>

<script setup>
import { computed, ref } from 'vue';
import { formatCurrency } from '@/utilities/formatting';

const props = defineProps({
  orderTotal: {
    type: Number,
    default: 0,
  },
});
const cashReceived = ref(0);
const cashback = computed(() => cashReceived.value - props.orderTotal);
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_buttons.scss';

.calculator {
  margin-bottom: 1rem;

  .calculator-row {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;

    line-height: 1.5rem;
    height: 4rem;
  }
}

input {
  width: 10rem;
  border: 0.2rem solid $primary-blue;
  //border-color: $primary-blue;
  border-radius: $button-border-radius;
  line-height: 2rem;
}

.styled-amount {
  color: $white;
  @include styled-button-base;
}

.order-total {
  background-color: $primary-green;
}

.cashback {
  background-color: $secondary-orange;
}
</style>
