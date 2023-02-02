<template>
  <h3>manage products</h3>
  <div class="buttons">
    <button type="button" class="btn-primary">
      <i class="icon-plus"></i>
      Product toevoegen
    </button>

    <button type="button" disabled class="btn-primary">
      <i class="icon-plus"></i>
      Product toevoegen
    </button>
  </div>
</template>

<script>
export default {
  name: 'ManageProductsPage',
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.buttons {
  > button {
    display: block;
    margin-bottom: 1rem;
  }
}
</style>
