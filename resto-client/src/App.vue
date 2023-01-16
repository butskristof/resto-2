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
  min-width: $min-body-width;
  max-width: $max-body-width;
  margin: auto;
}
</style>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

main {
  padding: 1rem;
  margin: auto 1rem;
  height: 100%;
  overflow: auto;

  background-color: $white;
  border-radius: $box-border-radius;
}
</style>
