<template>
  <nav>
    <router-link :to="{ name: 'ManageProducts' }">Gerechten</router-link>
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
  @include box-shadow;
  margin: calc(-1 * $box-padding);
  margin-bottom: $box-padding;
  display: flex;
  flex-direction: row;
  > * {
    flex-grow: 1;
    text-align: center;
  }

  // eslint-disable vue-scoped-css/no-unused-selector
  // disable eslint rule since a tags & .router-link-active won't appear explicitly in the template
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
  // eslint-enable
}
</style>
