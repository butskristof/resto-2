<template>
  <div class="manage">
    <div class="header">
      <div class="left">
        <LoadingIndicator v-if="loading">{{ loadingLabel }}</LoadingIndicator>
        <div v-if="listFailed">
          <div>
            Er liep iets mis bij het ophalen van de categorieën, probeer het
            later opnieuw.
          </div>
          <div>
            <pre>{{ listError }}</pre>
          </div>
        </div>
      </div>

      <div class="right">
        <button type="button" class="btn-icon" @click="openEditModal(null)">
          <i class="icon-plus"></i> Categorie toevoegen
        </button>
      </div>
    </div>

    <div class="list">
      <div v-if="listSuccess">
        <CategoryListItem
          v-for="category in categories"
          :key="category.id"
          :category="category"
          @edit="openEditModal"
          @delete="openDeleteModal"
        />
        <LoadNextPage
          v-if="listHasNextPage"
          entity="categorieën"
          @load-next-page="listFetchNextPage"
        />
      </div>
    </div>

    <EditCategoryModal
      v-if="showEditModal"
      :category="categoryToEdit"
      @close="closeEditModal"
    />
    <DeleteCategoryModal
      v-if="showDeleteModal"
      :category="categoryToDelete"
      @close="closeDeleteModal"
    />
  </div>
</template>

<script setup>
import { computed, ref } from 'vue';
import EditCategoryModal from '@/components/manage/categories/EditCategoryModal.vue';
import CategoryListItem from '@/components/manage/categories/CategoryListItem.vue';
import DeleteCategoryModal from '@/components/manage/categories/DeleteCategoryModal.vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import { useCategoriesQuery } from '@/composables/queries';
import LoadNextPage from '@/components/common/LoadNextPage.vue';

//#region list
const {
  categories,

  fetchNextPage: listFetchNextPage,
  hasNextPage: listHasNextPage,

  isLoading: listLoading,
  isFetching: listFetching,
  isFetchingNextPage: listFetchingNextPage,

  isSuccess: listSuccess,
  isError: listFailed,
  error: listError,
} = useCategoriesQuery();
const loading = computed(
  () => listLoading.value || listFetching.value || listFetchingNextPage.value,
);
const loadingLabel = computed(() => {
  if (listLoading.value || listFetchingNextPage.value)
    return 'Categorieën laden';
  else if (listFetching.value) return 'Categorieën bijwerken';
  return '';
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
  margin-bottom: 1rem;
}
</style>
