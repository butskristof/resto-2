<template>
  <div class="calculator">
    <div class="calculator-row">
      <div>Totaal bestelling</div>
      <div class="styled-amount order-total">{{ orderTotal }}</div>
    </div>
    <div class="calculator-row">
      <div>Cash ontvangen</div>
      <div>
        <input type="number" class="cash-received" v-model="cashReceived" />
      </div>
    </div>
    <div class="calculator-row">
      <div>Terug te geven</div>
      <div class="styled-amount cashback">{{ cashback }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CurrentOrderCalculator',
  props: {
    orderTotal: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      cashReceived: 0,
    };
  },
  computed: {
    cashback() {
      return this.cashReceived - this.orderTotal;
    },
  },
};
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
  //padding: 0.75rem;
  //border-radius: $button-border-radius;
  //font-weight: bold;
}

.order-total {
  background-color: $primary-green;
}

.cashback {
  background-color: $secondary-orange;
}
</style>
