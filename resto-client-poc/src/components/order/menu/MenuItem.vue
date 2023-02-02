<template>
  <div class="menu-item">
    <div class="product">
      <div class="name">
        {{ product.name }}
      </div>
      <div class="price">
        {{ product.price }}
      </div>
      <div class="add">
        <button type="button" @click="add">+</button>
      </div>
    </div>
    <div class="toppings" v-if="hasToppings">
      <div
        class="topping"
        v-for="topping in product.toppings"
        :key="topping.id"
      >
        <div class="name">
          {{ topping.name }}
        </div>
        <div class="price">+ {{ topping.price }}</div>
        <div class="add">
          <button type="button" @click="addTopping(topping.id)">+</button>
        </div>
      </div>
      <div>{{ selectedToppings.join(', ') }}</div>
    </div>
  </div>
</template>

<script setup>
import { computed, reactive } from 'vue';
import { useCurrentOrderStore } from '@/stores/current-order';

const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});
const hasToppings = computed(() => props.product?.toppings.length ?? false);
const selectedToppingIds = reactive([]);
const selectedToppings = computed(() =>
  props.product?.toppings
    .filter((t) => selectedToppingIds.includes(t.id))
    .map((t) => t.name),
);

const { addToOrder } = useCurrentOrderStore();

function add() {
  addToOrder({
    productId: props.product.id,
    toppingIds: [...selectedToppingIds],
  });
}

function addTopping(toppingId) {
  if (!selectedToppingIds.some((id) => id === toppingId))
    selectedToppingIds.push(toppingId);
}
</script>

<style scoped lang="scss">
.menu-item {
  margin-bottom: 1rem;
}

.product,
.topping {
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
  margin-bottom: 0.5rem;

  :first-child {
    margin-right: auto;
  }

  .add {
    width: 2rem;
    text-align: right;
  }
}
.toppings {
  margin-left: 1rem;
}
</style>
