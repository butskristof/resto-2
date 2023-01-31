<template>
  <div class="tabs">
    <router-link :to="{ name: 'ManageProducts' }">Products</router-link>
    <router-link :to="{ name: 'ManageToppings' }">Toppings</router-link>
    <router-link :to="{ name: 'ManageCategories' }">Categories</router-link>
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
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.tabs {
  a {
    padding: $button-padding;

    &.router-link-active {
      font-weight: bold;
    }
  }
}
</style>
