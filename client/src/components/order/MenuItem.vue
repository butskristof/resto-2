<template>
  <div class="menu-item" v-if="product">
    <div class="product">
      <button
        type="button"
        class="btn-product"
        :data-category="product.category.name"
      >
        {{ product.name }} - {{ formatCurrency(product.price) }}
      </button>
    </div>
    <div class="toppings" v-if="hasToppings">
      <template v-if="multipleToppings">
        <div class="checkbox-group">
          <div
            class="checkbox topping"
            v-for="topping in product.toppings"
            :key="topping.name"
          >
            <input
              type="checkbox"
              :value="topping"
              :id="'menu-item-' + product.id + '-topping-' + topping.name"
              v-model="selectedToppings"
            />
            <label :for="'menu-item-' + product.id + '-topping-' + topping.name"
              >{{ topping.name }} - {{ formatCurrency(topping.price) }}</label
            >
          </div>
        </div>
      </template>
      <template v-else>
        <div class="radio-group">
          <div
            class="radio-item topping"
            v-for="topping in product.toppings"
            :key="topping.name"
          >
            <input
              type="radio"
              :value="topping"
              :id="'menu-item-' + product.id + '-topping-' + topping.name"
              v-model="selectedToppings"
            />
            <label :for="'menu-item-' + product.id + '-topping-' + topping.name"
              >{{ topping.name }} - {{ formatCurrency(topping.price) }}</label
            >
          </div>
        </div>
      </template>
      <div class="actions">
        <button type="button" class="btn-add">Toevoegen</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/formatting';
import { computed, ref } from 'vue';

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
const selectedToppings = ref([]);
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_buttons.scss';
@import '@/styles/_forms.scss';

.menu-item {
  margin: auto 1rem 1rem 1rem;
}

.btn-product {
  @include reset-button;
  @include styled-button-base;
  width: 100%;
  padding: 1.5rem;

  @include category-styling;
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

.radio-item {
  @include styled-radio;

  label {
    vertical-align: bottom;
  }

  margin-bottom: 0.5rem;
}

.checkbox {
  @include styled-checkbox;

  label {
    vertical-align: bottom;
  }

  margin-bottom: 0.5rem;
}
</style>
