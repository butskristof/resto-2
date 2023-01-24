<template>
  <div class="manage">
    <div class="header">
      <div class="left">
        <h3>Manage categories</h3>
      </div>
      <div class="right">
        <button type="button" @click="open(null)">Add</button>
      </div>
    </div>
    <EditCategory v-if="show" :category="toEdit" @close="close" />
    <CategoriesList @edit="open" />
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
      show: false,
      toEdit: null,
    };
  },
  methods: {
    open(item) {
      this.toEdit = item;
      this.show = true;
    },
    close() {
      this.toEdit = null;
      this.show = false;
    },
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
