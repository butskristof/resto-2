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
            <input
              v-model="selectedDiscount"
              type="radio"
              :value="discount"
            />
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
          <div class="right">
            <CurrencyInput
              v-model="cashReceived"
              class="cash-received-input"
            />
          </div>
        </div>

        <div class="cash-return">
          <div class="left">Terug te geven</div>
          <div class="right">
            {{ formatCurrency(cashToReturn) }}
          </div>
        </div>
      </div>

      <div class="actions">
        <button
          type="button"
          class="btn-danger btn-icon"
          @click="reset"
        >
          <i class="icon-trash" />
          Wissen
        </button>
        <button
          type="button"
          class="btn-submit btn-icon"
          :disabled="!canCreate"
          @click="create"
        >
          <i class="icon-soup" />
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
import CurrencyInput from '@/components/common/form/inputs/CurrencyInput.vue';

const {
  total,
  discount: selectedDiscount,
  cashReceived,
  cashToReturn,
  canCreate,
} = storeToRefs(useCurrentOrderStore());
const { reset, create } = useCurrentOrderStore();
</script>

<style scoped lang="scss">
@use 'sass:color';
@use '@/styles/ui/_layout.scss';
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/_colors.scss';

.current-order {
  @include layout.flex-column;
  padding-left: padding-margin.$box-padding;
  padding-right: padding-margin.$box-padding;

  .ticket {
    flex-grow: 1;
  }

  .bottom {
    flex-shrink: 0;
    margin-bottom: padding-margin.$box-padding;

    .discount {
      @include layout.flex-row-space-between;
      flex-wrap: wrap;

      .left {
        margin-right: padding-margin.$box-padding;
      }

      .right {
        @include layout.flex-row;
        gap: padding-margin.$box-padding;
        margin-left: auto; // push right if wrapped

        label {
          cursor: pointer;
        }
      }

      margin-bottom: padding-margin.$box-padding;
    }

    .money-helper {
      //@include striped-rows;
      margin-top: calc(2 * #{padding-margin.$box-padding});

      .total,
      .cash-received,
      .cash-return {
        @include layout.flex-row-space-between;
        align-items: center;
        margin-bottom: padding-margin.$box-padding;

        .right {
          font-weight: 500;
        }
      }

      .cash-received .right {
        flex-basis: 125px;
      }
    }

    .actions {
      @include layout.flex-row-space-between;
      margin-top: calc(2 * #{padding-margin.$box-padding});

      .btn-submit {
        border-color: colors.$klj-green;
        background-color: colors.$klj-green;
        //color: white;
        font-weight: 500;

        &:hover {
          background-color: color.adjust(colors.$klj-green, $lightness: -10%);
          border-color: transparent;
          color: white;
        }

        &:focus {
          background-color: color.adjust(colors.$klj-green, $lightness: -10%);
          border-color: transparent;
        }

        &:disabled {
          background-color: color.adjust(colors.$klj-green, $lightness: 20%);
          border-color: transparent;
          color: colors.$body-text-color-lighter;
        }
      }
    }
  }
}
</style>
