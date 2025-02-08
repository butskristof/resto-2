<template>
  <header>
    <h1>Restaurantdag</h1>
  </header>
</template>

<style scoped lang="scss">
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/utilities/_typography.scss';
@import '@/styles/_colors.scss';

header {
  // default padding Y / centered X
  margin: $box-padding auto;
  @include brand-font;
  color: $background-contrast-color;
}

h1 {
  // resets default h1 Y + adds centered X
  margin: auto;
}
</style>
