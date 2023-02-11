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
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/_colors.scss';

main {
  background-color: $background-contrast-color;
  border-radius: $box-border-radius;
  padding: $box-padding;
  margin: auto $box-padding;
  //height: 100%;
  flex-grow: 1;

  overflow: auto;
}
</style>
