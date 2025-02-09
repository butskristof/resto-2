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
@use 'sass:color';
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/utilities/_typography.scss';
@use '@/styles/utilities/_general.scss';
@use '@/styles/_colors.scss';

nav {
  margin: calc(-1 * #{padding-margin.$box-padding});
  margin-bottom: padding-margin.$box-padding;

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
    $active: color.adjust($base, $lightness: -5%);
    $hover: color.adjust($base, $lightness: -10%);

    @include typography.reset-link;
    @include general.transition;
    border-bottom: 1px solid $hover;
    padding: padding-margin.$button-padding;
    color: colors.$body-text-color;

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
