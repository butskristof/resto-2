<template>
  <nav>
    <router-link :to="{ name: routeInfo.manage.products.name }">
      Gerechten
    </router-link>
    <router-link :to="{ name: routeInfo.manage.toppings.name }">
      Toppings
    </router-link>
    <router-link :to="{ name: routeInfo.manage.categories.name }">
      Categorieën
    </router-link>
  </nav>

  <RouterView />
</template>

<script setup>
import { RouterView } from 'vue-router';
import routeInfo from '@/router/route-info';
</script>

<style scoped lang="scss">
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/utilities/_typography.scss';
@import '@/styles/utilities/_general.scss';

nav {
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
    $base: white;
    $active: darken($base, 5%);
    $hover: darken($base, 10%);

    @include reset-link;
    @include transition;
    border-bottom: 1px solid $hover;
    padding: $button-padding;
    color: $body-text-color;

    &.router-link-active {
      background-color: $active;
      font-weight: 600;
    }

    &:hover {
      background-color: $hover;
    }
  }
  // eslint-enable
}
</style>
