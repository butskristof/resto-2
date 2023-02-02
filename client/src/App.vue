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

<style lang="scss">
@import '@/styles/_variables.scss';

#app {
  display: flex;
  flex-direction: column;
  height: 100%;
  //min-width: $min-body-width; // TODO only for order?
  max-width: $max-body-width;
  margin: auto;
}
</style>

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
