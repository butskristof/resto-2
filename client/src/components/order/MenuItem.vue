<template>
  <div class="menu-item">
    <div class="product">
      <button type="button" class="btn-product">
        Product - {{ formatCurrency(1.9) }}
      </button>
    </div>
    <div class="toppings" v-if="hasToppings">
      <div class="topping">
        <template v-if="singleTopping">
          <div class="radio-group" v-for="i in [...Array(2).keys()]" :key="i">
            <div class="radio-item">
              <input type="radio" :id="'topping' + i" :value="'topping' + i" />
              <label :for="'topping' + i">Topping {{ i }}</label>
            </div>
          </div>
        </template>
        <template v-else>
          <div class="checkbox" v-for="i in [...Array(2).keys()]" :key="i">
            <input
              type="checkbox"
              :id="'topping' + i"
              :name="'topping' + i"
              :value="'Topping ' + i"
            />
            <label :for="'topping' + i">Topping {{ i }}</label>
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
const hasToppings = Math.random() > 0.5;
const singleTopping = Math.random() > 0.5;
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
