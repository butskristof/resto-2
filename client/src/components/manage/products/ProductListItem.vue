<template>
  <div class="product">
    <div class="color" :style="colorBlockStyle"></div>
    <div class="row-1"></div>
    <div class="row-2"></div>
    <div class="name-toppings">
      <div class="name">{{ product.name }}</div>
      <div v-if="product.toppings.length > 0" class="toppings">
        <div>
          Toppings
          <span v-if="product.multipleToppingsAllowed">
            <strong> (meerdere toegelaten) </strong>
          </span>
        </div>
        <div
          v-for="topping in product.toppings"
          :key="topping.name"
          class="topping"
        >
          {{ topping.name }} ({{ formatCurrency(topping.price) }})
        </div>
      </div>
    </div>
    <div class="price">{{ formatCurrency(product.price) }}</div>
    <div class="actions">
      <button
        type="button"
        class="btn-blue btn-icon"
        @click="emit('edit', product)"
      >
        <i class="icon-edit"></i> Bewerken
      </button>
      <button
        type="button"
        class="btn-danger btn-icon"
        @click="emit('delete', product)"
      >
        <i class="icon-trash"></i> Verwijderen
      </button>
    </div>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/filters';
import { computed } from 'vue';

const emit = defineEmits(['edit', 'delete']);
const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});
const product = computed(() => props.product);
const colorBlockStyle = computed(() => ({
  'background-color': product.value.category.color,
}));
</script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/utilities/_typography.scss';
@import '@/styles/manage/_common.scss';
@import '@/styles/utilities/_general.scss';

.product {
  $row-padding: 0.5rem;

  @include flex-row;
  align-items: center;
  gap: $box-padding;
  @include striped-rows;
  padding: $row-padding 0;

  .color {
    @include category-color-block;
    width: 2rem;
    align-self: stretch;
    margin-top: calc(-1 * $row-padding);
    margin-bottom: calc(-1 * $row-padding);
  }

  .name-toppings {
    flex-grow: 1;

    .toppings {
      @include extra-info-text;
      .topping {
        margin-left: $box-padding;
      }
    }
  }

  .actions {
    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
