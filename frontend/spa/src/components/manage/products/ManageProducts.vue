<template>
  <ManageBase>
    <template #header>
      <div class="left">
        <LoadingIndicator v-if="loading">{{ loadingLabel }}</LoadingIndicator>
        <div v-if="listFailed">
          <div>Er liep iets mis bij het ophalen van de toppings, probeer het later opnieuw.</div>
          <div>
            <pre>{{ listError }}</pre>
          </div>
        </div>
      </div>

      <div class="right">
        <button
          type="button"
          class="btn-blue btn-icon"
          @click="openEditModal(null)"
        >
          <i class="icon-plus" /> Gerecht toevoegen
        </button>
      </div>
    </template>

    <template #list>
      <div v-if="listSuccess">
        <ProductListItem
          v-for="product in products"
          :key="product.id"
          :product="product"
          @edit="openEditModal"
          @delete="openDeleteModal"
        />
        <LoadNextPage
          v-if="listHasNextPage"
          entity="gerechten"
          @load-next-page="listFetchNextPage"
        />
      </div>
    </template>

    <template #modals>
      <EditProductModal
        v-if="showEditModal"
        :product="productToEdit"
        @close="closeEditModal"
      />
      <DeleteProductModal
        v-if="showDeleteModal"
        :product="productToDelete"
        @close="closeDeleteModal"
      />
    </template>
  </ManageBase>
</template>

<script setup>
import { computed, ref } from 'vue';
import ProductListItem from '@/components/manage/products/ProductListItem.vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import EditProductModal from '@/components/manage/products/EditProductModal.vue';
import { useProductsQuery } from '@/composables/queries';
import DeleteProductModal from '@/components/manage/products/DeleteProductModal.vue';
import LoadNextPage from '@/components/common/LoadNextPage.vue';
import ManageBase from '@/components/manage/common/ManageBase.vue';

//#region list
const {
  products,

  fetchNextPage: listFetchNextPage,
  hasNextPage: listHasNextPage,

  isLoading: listLoading,
  isFetching: listFetching,
  isFetchingNextPage: listFetchingNextPage,

  isSuccess: listSuccess,
  isError: listFailed,
  error: listError,
} = useProductsQuery();
const loading = computed(
  () => listLoading.value || listFetching.value || listFetchingNextPage.value,
);
const loadingLabel = computed(() => {
  if (listLoading.value || listFetchingNextPage.value) return 'Gerechten laden';
  else if (listFetching.value) return 'Gerechten bijwerken';
  return '';
});
//#endregion

//#region create & update modal
const showEditModal = ref(false);
const productToEdit = ref(null);
const openEditModal = (product) => {
  productToEdit.value = product;
  showEditModal.value = true;
};
const closeEditModal = () => {
  productToEdit.value = null;
  showEditModal.value = false;
};
//#endregion

//#region delete
const productToDelete = ref(null);
const showDeleteModal = computed(() => productToDelete.value != null);
const openDeleteModal = (product) => (productToDelete.value = product);
const closeDeleteModal = () => (productToDelete.value = null);
//#endregion
</script>

<style scoped lang="scss"></style>
