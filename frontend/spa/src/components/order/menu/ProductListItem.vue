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
import { computed, ref, watch } from 'vue';
import { getTextColor } from '@/utilities/color';

const emit = defineEmits(['add']);
const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
  orderKey: {
    type: Number,
    default: 0,
  },
});
const categoryColorStyle = computed(() => ({
  'background-color': props.product.category.color,
  color: getTextColor(props.product.category.color),
}));

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

//#region topping selection

const selectedToppingIds = ref([]);

const inputType = computed(() =>
  props.product.multipleToppingsAllowed ? 'checkbox' : 'radio',
);

function setDefaultToppingSelection() {
  // if there are toppings available, and the list is rendered as a radio,
  // select the first option by default
  if (inputType.value === 'radio' && props.product.toppings.length > 0)
    selectedToppingIds.value = props.product.toppings[0].id;
  else selectedToppingIds.value = [];
}

// when the current order resets, reset the selected toppings
watch(
  () => props.orderKey,
  () => {
    setDefaultToppingSelection();
  },
);
watch(
  () => props.product,
  () => {
    setDefaultToppingSelection();
  },
  { immediate: true },
);

//#endregion
</script>

<style scoped lang="scss">
@use '@/styles/ui/_layout.scss';
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/_colors.scss';

.product {
  margin-bottom: calc(#{padding-margin.$box-padding} * 2);
}

.name-price {
  @include layout.flex-row-space-between;
  padding: padding-margin.$box-padding calc(2 * #{padding-margin.$box-padding})
    padding-margin.$box-padding padding-margin.$box-padding;
  border-radius: padding-margin.$button-border-radius;
  border: padding-margin.$button-border-width solid
    colors.$background-contrast-color-darker;

  font-weight: 500;
}

.toppings-actions {
  margin: 0 padding-margin.$box-padding;
  padding: padding-margin.$box-padding;
  background-color: colors.$background-contrast-color-darker;
  border-bottom-left-radius: padding-margin.$button-border-radius;
  border-bottom-right-radius: padding-margin.$button-border-radius;

  .toppings {
    //margin-bottom: $box-padding;

    .topping {
      @include layout.flex-row-space-between;
      margin-bottom: padding-margin.$box-padding;
    }
  }

  .actions {
    @include layout.flex-row;
    justify-content: flex-end;

    button {
      font-weight: 500;
    }
  }
}

.btn-add {
  background-color: white;
  border-color: white;

  &:hover {
    background-color: white;
    border-color: colors.$body-text-color;
  }
}
</style>
