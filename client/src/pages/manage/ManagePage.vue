<template>
  <nav>
    <router-link :to="{ name: 'ManageProducts' }">Maaltijden</router-link>
    <router-link :to="{ name: 'ManageToppings' }">Toppings</router-link>
    <router-link :to="{ name: 'ManageCategories' }">Categorieën</router-link>
  </nav>

  <RouterView />
</template>

<script setup>
import { RouterView } from 'vue-router';
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

nav {
  margin: calc(-1 * $box-padding);
  margin-bottom: $box-padding;
  display: flex;
  flex-direction: row;
  > * {
    flex-grow: 1;
    text-align: center;
  }

  a {
    @include reset-link;
    @include transition;
    border-bottom: 1px solid $gray-light;
    padding: $button-padding;
    color: $body-text-color;

    &:hover {
      background-color: $gray-lighter;
    }

    &.router-link-active {
      background-color: $gray-lighter;
      font-weight: bold;
    }
  }
}
</style>
