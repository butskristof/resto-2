<template>
  <AppHeader />

  <main>
    <RouterView />
  </main>

  <AppFooter />
</template>

<script setup>
import { RouterView } from 'vue-router';
import AppHeader from '@/components/ui/AppHeader.vue';
import AppFooter from '@/components/ui/AppFooter.vue';
</script>

<style scoped lang="scss">
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/_colors.scss';

main {
  background-color: colors.$background-contrast-color;
  border-radius: padding-margin.$box-border-radius;
  padding: padding-margin.$box-padding;
  margin: auto padding-margin.$box-padding;
  //height: 100%;
  flex-grow: 1;

  overflow: auto;
}
</style>
