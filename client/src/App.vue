<template>
  <header>
    <h1>Restaurantdag</h1>
  </header>

  <main>
    <RouterView />
  </main>

  <footer>&copy; KLJ Wiekevorst</footer>
</template>

<script setup>
import { RouterView } from 'vue-router';
</script>

<style>
#app {
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem;
}
</style>

<style lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

header {
  margin-bottom: 1rem;

  h1 {
    color: $meta-text-color;
    @include klj-font;

    margin: 0;
  }
}

main {
  height: 88vh;

  padding: 1rem;
  width: 100%;
  border-radius: 0.75rem;

  background-color: $white;
}

footer {
  margin-top: 1rem;

  color: $meta-text-color;
}
</style>
