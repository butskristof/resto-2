<template>
  <div class="manage">
    <div class="header">
      <div class="left">
        <h3>Manage categories</h3>
      </div>
      <div class="right">
        <button type="button" @click="show = true">Add</button>
      </div>
    </div>
    <EditCategory :show="show" />
    <CategoriesList />
  </div>
</template>

<script>
import CategoriesList from '@/components/manage/categories/CategoriesList.vue';
import EditCategory from '@/components/manage/categories/EditCategory.vue';

export default {
  name: 'ManageCategoriesPage',
  components: { EditCategory, CategoriesList },
  data() {
    return {
      show: true,
    };
  },
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
