<template>
  <div class="menu-item" v-if="product">
    <div class="product">
      <button type="button" class="btn-product">
        {{ product.name }} - {{ formatCurrency(product.price) }}
      </button>
    </div>
    <div class="toppings" v-if="hasToppings">
      <div
        class="topping"
        v-for="topping in product.toppings"
        :key="topping.name"
      >
        <template v-if="multipleToppings">
          <div class="checkbox">
            <input type="checkbox" />
            <label
              >{{ topping.name }} - {{ formatCurrency(topping.price) }}</label
            >
          </div>
        </template>
        <template v-else>
          <div class="radio-group">
            <input type="radio" />
            <label
              >{{ topping.name }} - {{ formatCurrency(topping.price) }}</label
            >
          </div>
        </template>
      </div>
      <div class="actions">
        <button type="button" class="btn-add">Toevoegen</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/formatting';
import { computed } from 'vue';

const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});

const hasToppings = computed(() => props.product?.toppings.length ?? false);
const multipleToppings = computed(
  () => props.product?.multipleToppings ?? false,
);
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_buttons.scss';

.menu-item {
  margin: auto 1rem 1rem 1rem;
}

.btn-product {
  @include reset-button;
  @include styled-button-base;
  width: 100%;
  padding: 1.5rem;

  background-color: $secondary-brown;
  color: $white;
}

.toppings {
  background-color: #eee;
  margin: auto 2rem 1rem 2rem;
  padding: 1rem;
  border-bottom-left-radius: $button-border-radius;
  border-bottom-right-radius: $button-border-radius;

  .actions {
    display: flex;
    flex-direction: row;
    justify-content: flex-end;

    .btn-add {
      @include reset-button;
      @include styled-button-base;

      font-size: 90%;
      background-color: $white;
      border: 0.125rem solid $primary-green;
      color: $primary-green;

      &:hover {
        background-color: $primary-green;
        color: $white;
      }
    }
  }
}
</style>
