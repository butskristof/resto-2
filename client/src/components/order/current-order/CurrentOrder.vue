<template>
  <div class="current-order">
    <CurrentOrderTicket class="ticket" />

    <div class="bottom">
      <div class="discount">
        <div class="left">Bestelling voor</div>
        <div class="right">
          <label
            v-for="discount in Object.values(ORDER_DISCOUNT)"
            :key="discount.value"
          >
            <input v-model="selectedDiscount" type="radio" :value="discount" />
            {{ discount.displayValue }}
          </label>
        </div>
      </div>

      <div class="money-helper">
        <div class="total">
          <div class="left">Totaal bestelling</div>
          <div class="right">
            {{ formatCurrency(total) }}
          </div>
        </div>

        <div class="cash-received">
          <div class="left">Cash ontvangen</div>
          <div class="right">123</div>
        </div>

        <div class="cash-return">
          <div class="left">Terug te geven</div>
          <div class="right">
            {{ formatCurrency(0) }}
          </div>
        </div>
      </div>

      <div class="actions">
        <button type="button" class="btn-danger btn-icon" @click="reset">
          <i class="icon-trash"></i>
          Wissen
        </button>
        <button type="button" class="btn-submit btn-icon">
          <i class="icon-printer"></i>
          Bestellen
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/filters';
import CurrentOrderTicket from '@/components/order/current-order/CurrentOrderTicket.vue';
import { useCurrentOrderStore } from '@/stores/current-order';
import { storeToRefs } from 'pinia';
import { ORDER_DISCOUNT } from '@/utilities/order-discount';

const { total, discount: selectedDiscount } = storeToRefs(
  useCurrentOrderStore(),
);
const { reset } = useCurrentOrderStore();
</script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/_colors.scss';

.current-order {
  @include flex-column;
  padding-left: $box-padding;
  padding-right: $box-padding;

  .ticket {
    flex-grow: 1;
  }

  .bottom {
    flex-shrink: 0;
    margin-bottom: $box-padding;

    .discount {
      @include flex-row-space-between;
      flex-wrap: wrap;

      .left {
        margin-right: $box-padding;
      }

      .right {
        @include flex-row;
        gap: $box-padding;
        margin-left: auto; // push right if wrapped

        label {
          cursor: pointer;
        }
      }

      margin-bottom: $box-padding;
    }

    .money-helper {
      //@include striped-rows;
      margin-top: calc(2 * $box-padding);

      .total,
      .cash-received,
      .cash-return {
        @include flex-row-space-between;
        margin-bottom: $box-padding;

        .right {
          font-weight: 500;
        }
      }
    }

    .actions {
      @include flex-row-space-between;
      margin-top: calc(2 * $box-padding);

      .btn-submit {
        border-color: $klj-green;
        background-color: $klj-green;
        //color: white;
        font-weight: 500;

        &:hover {
          background-color: darken($klj-green, 10%);
          border-color: transparent;
          color: white;
        }

        &:focus {
          background-color: darken($klj-green, 10%);
          border-color: transparent;
        }
      }
    }
  }
}
</style>
