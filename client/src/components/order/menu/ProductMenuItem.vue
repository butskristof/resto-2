<template>
  <div class="product">
    <div class="name-price">
      <div class="name">{{ product.name }}</div>
      <div class="price">{{ formatCurrency(product.price) }}</div>
    </div>
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
      <button type="button" class="btn-blue" @click="add">
        <i class="icon-plus"></i>
        Toevoegen
      </button>
    </div>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/filters';
import { computed, ref } from 'vue';

const emit = defineEmits(['add']);
const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});

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
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.product {
  @include striped-rows;

  padding: $box-padding;

  .name-price,
  .topping {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
  }

  .actions {
    display: flex;
    flex-direction: row;
    justify-content: flex-end;

    margin-top: $box-padding;
  }
}
</style>
