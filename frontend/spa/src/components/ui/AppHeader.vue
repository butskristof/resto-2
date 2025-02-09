<template>
  <header>
    <h1>Restaurantdag</h1>
  </header>
</template>

<style scoped lang="scss">
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/utilities/_typography.scss';
@use '@/styles/_colors.scss';

header {
  // default padding Y / centered X
  margin: padding-margin.$box-padding auto;
  @include typography.brand-font;
  color: colors.$background-contrast-color;
}

h1 {
  // resets default h1 Y + adds centered X
  margin: auto;
}
</style>
