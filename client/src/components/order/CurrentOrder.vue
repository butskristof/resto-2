<template>
  <div class="current-order">
    <h2>Order</h2>
    <CurrentOrderTicket />
    <div class="discounts">
      <div>Bestelling voor</div>
      <div class="radio-group">
        <div class="radio-item">
          <input
            type="radio"
            id="order-for-item-customer"
            :value="ORDER_FOR[0]"
            v-model="orderFor"
          />
          <label for="order-for-item-customer">Klant</label>
        </div>
        <div class="radio-item">
          <input
            type="radio"
            id="order-for-item-helper"
            :value="ORDER_FOR[1]"
            v-model="orderFor"
          />
          <label for="order-for-item-helper">Helper</label>
        </div>
        <div class="radio-item">
          <input
            type="radio"
            id="order-for-item-member"
            :value="ORDER_FOR[2]"
            v-model="orderFor"
          />
          <label for="order-for-item-member">Leiding</label>
        </div>
      </div>
    </div>
    <CurrentOrderCalculator />
    <div class="actions">
      <RestoButton theme="red">Wissen</RestoButton>
      <RestoButton theme="green">Bestellen</RestoButton>
    </div>
  </div>
</template>

<script setup>
import RestoButton from '@/components/common/RestoButton.vue';
import CurrentOrderCalculator from '@/components/order/CurrentOrderCalculator.vue';
import CurrentOrderTicket from '@/components/order/CurrentOrderTicket.vue';
import { ref } from 'vue';

const ORDER_FOR = ['CUSTOMER', 'HELPER', 'MEMBER'];
const orderFor = ref(ORDER_FOR[0]);
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.current-order {
  display: flex;
  flex-direction: column;
}

h2 {
  @include styled-h2;
}

.discounts {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;

  line-height: 1.5rem;
  padding: 0.5rem 0;

  .radio-group {
    display: inline-flex;
    flex-direction: row;

    .radio-item {
      margin-left: 1rem;
    }
  }
}

.actions {
  margin-top: 1rem;

  display: flex;
  flex-direction: row;
  justify-content: space-between;

  button {
    font-weight: initial;
    font-size: 125%;
  }
}
</style>
