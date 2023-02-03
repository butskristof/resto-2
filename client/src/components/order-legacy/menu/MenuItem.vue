<template>
  <div class="menu-item">
    <div class="header">
      <div class="name">
        {{ product.name }}
      </div>
      <div class="price">
        {{ product.price }}
      </div>
    </div>
    <div class="extras">
      <div class="toppings">
        <div v-for="topping in toppings" :key="topping.id" class="topping">
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
          <div class="price">+ {{ topping.price }}</div>
        </div>
      </div>
      <div class="actions">
        <button type="button" @click="add">Toevoegen</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MenuItem',
};
</script>

<script setup>
import { computed, ref } from 'vue';
import { useCurrentOrderStore } from '@/stores/current-order';

const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});

const inputType = computed(() =>
  props.product.multipleToppings ? 'checkbox' : 'radio',
);

const toppings = computed(() => props.product.toppings);
const selectedToppingIds = ref([]);

const { addToOrder } = useCurrentOrderStore();

function add() {
  // when using radio buttons (so only a single topping is allowed), the v-model
  // will be set to a string with the topping ID instead of an array
  // when using checkboxes, it'll update the existing array in the ref,
  // but we should spread it again to copy the values in the array, not the reference to the array
  // otherwise, modifying the selection will also update the value in the store
  const toppingIds = Array.isArray(selectedToppingIds.value)
    ? [...selectedToppingIds.value]
    : [selectedToppingIds.value];
  addToOrder({
    productId: props.product.id,
    toppingIds: toppingIds,
  });
}
</script>

<style scoped lang="scss">
.menu-item {
  border-bottom: 1px solid black;
  padding-bottom: 1rem;
  margin: 1rem auto;
}

.header,
.topping {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.header {
}

.extras {
}

.actions {
  margin-top: 1rem;
  text-align: right;
}
</style>
