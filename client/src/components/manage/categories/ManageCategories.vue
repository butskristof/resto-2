<template>
  <div class="manage">
    <div class="header">
      <div class="left">
        <div v-if="listLoading">Loading...</div>
        <div v-else-if="listFetching">Refreshing...</div>
      </div>
      <div class="right">
        <IconButton @click="openEditModal(null)">
          <template #icon>
            <Plus />
          </template>
          Categorie toevoegen
        </IconButton>
      </div>
    </div>

    <div class="list">
      <div v-if="listFailed">Error: {{ listError.message }}</div>
      <div v-if="listSuccess">
        <CategoryListItem
          v-for="category in categories.results"
          :key="category.id"
          :category="category"
          @edit="openEditModal"
          @delete="openDeleteModal"
        />
      </div>
    </div>

    <EditCategoryModal
      v-if="showEditModal"
      @close="closeEditModal"
      :category="categoryToEdit"
    />
    <DeleteCategoryModal
      v-if="showDeleteModal"
      @close="closeDeleteModal"
      :category="categoryToDelete"
    />
  </div>
</template>

<script setup>
import { computed, ref } from 'vue';
import EditCategoryModal from '@/components/manage/categories/EditCategoryModal.vue';
import { useQuery } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import CategoriesService from '@/services/resto-api/categories.service';
import CategoryListItem from '@/components/manage/categories/CategoryListItem.vue';
import DeleteCategoryModal from '@/components/manage/categories/DeleteCategoryModal.vue';
import IconButton from '@/components/common/IconButton.vue';
import { Plus } from 'lucide-vue-next';

//#region list
const {
  data: categories,
  isFetching: listFetching,
  isLoading: listLoading,
  isError: listFailed,
  isSuccess: listSuccess,
  error: listError,
} = useQuery({
  queryKey: QUERY_KEYS.CATEGORIES,
  queryFn: async () => (await CategoriesService.get()).data,
});
//#endregion

//#region create & update modal
const showEditModal = ref(false);
let categoryToEdit = ref(null);
const openEditModal = (category) => {
  categoryToEdit.value = category;
  showEditModal.value = true;
};
const closeEditModal = () => {
  categoryToEdit.value = null;
  showEditModal.value = false;
};
//#endregion

//#region delete
const categoryToDelete = ref(null);
const showDeleteModal = computed(() => categoryToDelete.value != null);
const openDeleteModal = (category) => {
  categoryToDelete.value = category;
};
const closeDeleteModal = () => {
  categoryToDelete.value = null;
};
//#endregion
</script>

<style scoped lang="scss">
.header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-top: 1rem;
  margin-bottom: 1rem;
}
</style>
