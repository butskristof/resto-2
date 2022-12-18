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

.radio-item {
  $color1: $white;
  $color2: $primary-blue;

  input[type='radio'] {
    position: absolute;
    opacity: 0;

    + label {
      &:before {
        content: '';
        background: $color1;
        border-radius: 100%;
        border: 1px solid darken($color1, 25%);
        display: inline-block;
        width: 1.2rem;
        height: 1.2rem;
        position: relative;
        margin-right: 0.4rem;
        vertical-align: top;
        cursor: pointer;
        text-align: center;
      }
    }

    &:checked {
      + label {
        &:before {
          background-color: $color2;
          box-shadow: inset 0 0 0 4px $color1;
        }
      }
    }

    &:focus {
      + label {
        &:before {
          outline: none;
          border-color: $color2;
        }
      }
    }

    &:disabled {
      + label {
        &:before {
          box-shadow: inset 0 0 0 4px $color1;
          border-color: darken($color1, 25%);
          background: darken($color1, 25%);
        }
      }
    }

    + label {
      cursor: pointer;

      &:empty {
        &:before {
          margin-right: 0;
        }
      }
    }
  }
}
</style>
