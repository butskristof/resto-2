<template>
  <div class="current-order">
    <h2>Order</h2>
  </div>
</template>

<script>
export default {
  name: 'CurrentOrder',
};
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

h2 {
  @include styled-h2;
}
</style>
