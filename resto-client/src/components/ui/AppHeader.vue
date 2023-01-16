<template>
  <header>
    <h1>Restaurantdag</h1>
  </header>
</template>

<script>
export default {
  name: 'AppHeader',
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

header {
  margin: 0.75rem auto;

  @include brand-font;
  color: $meta-text-color;
  text-align: center;
}

h1 {
  margin: auto;
}
</style>
