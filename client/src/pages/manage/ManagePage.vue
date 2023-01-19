<template>
  <div class="header">
    <div class="left">
      <h2>Manage</h2>
    </div>
    <div class="right">
      <router-link :to="{ name: 'ManageProducts' }">Products</router-link>
      <router-link :to="{ name: 'ManageToppings' }">Toppings</router-link>
      <router-link :to="{ name: 'ManageCategories' }">Categories</router-link>
    </div>
  </div>
  <RouterView />
</template>

<script>
import { RouterView } from 'vue-router';

export default {
  name: 'ManagePage',
  components: [RouterView],
};
</script>

<style scoped lang="scss">
.header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.right * {
  &:not(:last-of-type) {
    margin-right: 1rem;
  }
}
</style>
