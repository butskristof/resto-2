<template>
  <div class="ticket">
    <table class="ticket-table">
      <thead>
        <tr>
          <th>Beschrijving</th>
          <th class="quantity">#</th>
          <th class="unit-price">EHP</th>
          <th class="subtotal">Totaal</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(orderLine, index) in orderLines" :key="index">
          <td class="description">
            <div class="wrapper">
              {{ orderLine.product.name }}
              <div v-if="orderLine.toppings.length > 0">
                <div
                  v-for="topping in orderLine.toppings"
                  :key="topping.id"
                  class="topping"
                >
                  {{ topping.name }}
                </div>
              </div>
            </div>
          </td>
          <td class="quantity">
            <div class="wrapper">
              <span class="amount">{{ orderLine.count }}</span>
              <button
                type="button"
                class="btn-increment"
                @click="increment(index)"
              >
                +
              </button>
              <button
                type="button"
                class="btn-decrement"
                @click="decrement(index)"
              >
                -
              </button>
            </div>
          </td>
          <td class="unit-price">{{ formatCurrency(orderLine.unitPrice) }}</td>
          <td class="subtotal">{{ formatCurrency(orderLine.totalPrice) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/filters';
import { useCurrentOrderStore } from '@/stores/current-order';
import { storeToRefs } from 'pinia';

const { orderLines } = storeToRefs(useCurrentOrderStore());
const { increment, decrement } = useCurrentOrderStore();
</script>

<style scoped lang="scss">
@import '@/styles/_colors.scss';
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/utilities/_general.scss';
@import '@/styles/ui/_layout.scss';

.ticket {
  border: 2px solid $body-text-color;
  overflow: auto;

  font-family: monospace;
  min-height: 200px;
  margin-bottom: 1rem;
}

.ticket-table {
  border-collapse: collapse;
  width: 100%;
  table-layout: fixed;

  $money-column-width: 90px;
  $quantity-column-width: 80px;

  td,
  th {
    vertical-align: baseline;
    &:first-child {
      padding-left: $box-padding;
    }

    &:last-child {
      padding-right: $box-padding;
    }
  }

  tbody tr {
    @include striped-rows;
    //line-height: 2rem;
  }

  thead {
    text-align: left;

    th {
      padding-top: $button-padding-y;
      padding-bottom: $button-padding-y;
      background-color: $background-color;
      color: $background-contrast-color;
    }
  }

  .description {
    .wrapper {
      display: block;
      line-height: initial;
      padding-top: $box-padding;
      padding-bottom: $box-padding;

      .topping {
        padding-left: $box-padding;
      }
    }
  }

  .unit-price,
  .subtotal {
    width: $money-column-width;
    text-align: right;
  }

  .quantity {
    width: $quantity-column-width;
    text-align: right;
    .wrapper {
      display: flex;
      align-items: center;
      justify-content: flex-end;
      @include flex-row;
      gap: 0.25rem;

      .amount {
        margin-right: 0.25rem;
      }

      .btn-increment,
      .btn-decrement {
        color: white;
        font-weight: 600;
        padding: 0.25rem 0.5rem;
        border-color: transparent;
      }

      .btn-increment {
        background-color: $klj-green;
      }

      .btn-decrement {
        background-color: $klj-red;
      }
    }
  }
}
</style>
