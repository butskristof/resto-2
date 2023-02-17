<template>
  <div class="order">
    <div class="row">
      <div class="left">
        <div class="timestamp">
          {{ formatTimestamp(order.timestamp) }}
        </div>
        <div class="id">{{ order.id }}</div>
      </div>
      <div class="right">
        <div class="order-total">
          {{ formatCurrency(order.orderTotal) }}
          <div
            v-if="order.discount != ORDER_DISCOUNT.None.value"
            class="discount"
          >
            (korting: {{ ORDER_DISCOUNT[order.discount].displayValue }})
          </div>
        </div>
        <button type="button" @click="print">
          <i class="icon-printer"></i>
          Print
        </button>
      </div>
    </div>
    <div class="order-lines">
      <div
        v-for="orderLine in order.orderLines"
        :key="orderLine.id"
        class="order-line"
      >
        <span class="quantity">{{ orderLine.quantity }} x</span>
        <span>&nbsp;</span>
        <span class="product">
          {{ orderLine.product.name }}
        </span>
        <span>&nbsp;</span>
        <span class="toppings">
          {{ orderLine.toppings.map((t) => t.name).join(', ') }}
        </span>
      </div>
    </div>
  </div>
</template>

<script setup>
import OrdersService from '@/services/resto-api/orders.service';
import { formatCurrency, formatTimestamp } from '@/utilities/filters';
import { ORDER_DISCOUNT } from '@/utilities/order-discount';

const props = defineProps({
  order: {
    type: Object,
    required: true,
  },
});
function print() {
  return OrdersService.print(props.order.id);
}
</script>

<style scoped lang="scss">
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_general.scss';
@import '@/styles/utilities/_typography.scss';

.order {
  padding: $box-padding;
  //margin-bottom: $box-padding;
  @include striped-rows;

  .row {
    @include flex-row-space-between;

    .right {
      @include flex-row;
      gap: $box-padding;

      .order-total {
        text-align: right;

        .discount {
          @include extra-info-text;
        }
      }
    }
  }

  .id,
  .toppings {
    @include extra-info-text;
  }

  .order-lines {
    margin-top: $box-padding;
    margin-left: $box-padding;

    //.order-line {
    //  @include flex-row;
    //  gap: $box-padding;
    //}
  }
}
</style>
