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
        <div class="topping" v-for="topping in toppings" :key="topping.id">
          <div class="name">{{ topping.name }}</div>
          <div class="price">+ {{ topping.price }}</div>
        </div>
      </div>
      <div class="actions">
        <button type="button" @click="add">Toevoegen</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed, ref } from 'vue';

const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});
const toppings = computed(() => props.product?.toppings);
const selectedToppingIds = ref([]);

function add() {}
</script>

<style scoped lang="scss">
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
  text-align: right;
}
</style>
