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
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

main {
  @include box-shadow;
  padding: $box-padding;
  margin: auto $box-padding;
  height: 100%;
  overflow: auto;

  background-color: $white;
  border-radius: $box-border-radius;
  color: $body-text-color;
}
</style>
