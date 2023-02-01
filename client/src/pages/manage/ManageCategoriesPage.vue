<template>
  <div class="manage">
    <div class="header">
      <div class="left">
        <h3>Manage categories</h3>
      </div>
      <div class="right">
        <button type="button" @click="open(null)">Categorie toevoegen</button>
      </div>
    </div>
    <EditCategoryModal v-if="show" @close="close" :category="toEdit" />
    <CategoriesList @edit="open" />
  </div>
</template>

<script>
import CategoriesList from '@/components/manage/categories/CategoriesList.vue';
import EditCategoryModal from '@/components/manage/categories/EditCategoryModal.vue';

export default {
  name: 'ManageCategoriesPage',
  components: { EditCategoryModal, CategoriesList },
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
