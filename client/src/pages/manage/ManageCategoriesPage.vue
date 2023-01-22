<template>
  <div class="header">
    <div class="left">
      <h3>Manage categories</h3>
    </div>
    <div class="right">
      <button type="button">Add</button>
    </div>
  </div>
  <CategoriesList />
</template>

<script>
import CategoriesList from '@/components/manage/categories/CategoriesList.vue';

export default {
  name: 'ManageCategoriesPage',
  components: { CategoriesList },
};
</script>

<style scoped lang="scss">
.header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
</style>
