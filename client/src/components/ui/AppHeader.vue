<template>
  <header>
    <h1>Restaurantdag</h1>
  </header>
</template>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

header {
  margin: $box-padding auto;

  @include brand-font;
  color: $meta-text-color;
}

h1 {
  font-size: 2.1rem;
  margin: auto; // also resets top & bottom margin of h1
}
</style>
