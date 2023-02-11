<template>
  <div class="product">
    <div class="name-price" :style="categoryColorStyle">
      <div class="name">{{ product.name }}</div>
      <div class="price">{{ formatCurrency(product.price) }}</div>
    </div>
    <div class="toppings-actions">
      <div v-if="product.toppings.length > 0" class="toppings">
        <div
          v-for="topping in product.toppings"
          :key="topping.id"
          class="topping"
        >
          <div class="left">
            <label>
              <input
                v-model="selectedToppingIds"
                :type="inputType"
                :value="topping.id"
              />
              {{ topping.name }}
            </label>
          </div>
          <div class="price">+ {{ formatCurrency(topping.price) }}</div>
        </div>
      </div>

      <div class="actions">
        <button type="button" class="btn-add" @click="add">
          <i class="icon-plus"></i>
          Toevoegen
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/filters';
import { computed, ref } from 'vue';
import { getTextColor } from '@/utilities/color';

const emit = defineEmits(['add']);
const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});
const categoryColorStyle = computed(() => ({
  'background-color': props.product.category.color,
  color: getTextColor(props.product.category.color),
}));

const selectedToppingIds = ref([]);

const inputType = computed(() =>
  props.product.multipleToppingsAllowed ? 'checkbox' : 'radio',
);

function add() {
  // when using radio buttons (so only a single topping is allowed), the v-model
  // will be set to a string with the topping ID instead of an array
  // when using checkboxes, it'll update the existing array in the ref,
  // but we should spread it again to copy the values in the array, not the reference to the array
  // otherwise, modifying the selection will also update the value in the store
  const toppingIds = Array.isArray(selectedToppingIds.value)
    ? [...selectedToppingIds.value]
    : [selectedToppingIds.value];
  emit('add', {
    productId: props.product.id,
    toppingIds: toppingIds,
  });
}
</script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/_colors.scss';

.product {
  margin-bottom: calc($box-padding * 2);
}

.name-price {
  @include flex-row-space-between;
  padding: $box-padding calc(2 * $box-padding) $box-padding $box-padding;
  border-radius: $button-border-radius;
  border: $button-border-width solid $background-contrast-color-darker;

  font-weight: 500;
}

.toppings-actions {
  margin: 0 $box-padding;
  padding: $box-padding;
  background-color: $background-contrast-color-darker;
  border-bottom-left-radius: $button-border-radius;
  border-bottom-right-radius: $button-border-radius;

  .toppings {
    //margin-bottom: $box-padding;

    .topping {
      @include flex-row-space-between;
      margin-bottom: $box-padding;
    }
  }

  .actions {
    @include flex-row;
    justify-content: flex-end;

    button {
      font-weight: 500;
    }
  }
}

.btn-add {
  background-color: white;

  &:hover {
    background-color: white;
  }
}
</style>
