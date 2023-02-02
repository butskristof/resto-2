<template>
  <div class="manage">
    <div class="header">
      <div class="left">
        <LoadingIndicator v-if="loading">{{ loadingLabel }}</LoadingIndicator>
        <div v-if="listFailed">
          <div>
            Er liep iets mis bij het ophalen van de toppings, probeer het later
            opnieuw.
          </div>
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
          <i class="icon-plus"></i> Gerecht toevoegen
        </button>
      </div>
    </div>

    <div class="list">
      <div v-if="listSuccess">
        <ProductListItem
          v-for="product in products.results"
          :key="product.id"
          :product="product"
          @edit="openEditModal"
          @close="closeEditModal"
        />
      </div>
    </div>

    <EditProductModal
      v-if="showEditModal"
      :product="productToEdit"
      @close="closeEditModal"
    />
  </div>
</template>

<script setup>
import { computed, ref } from 'vue';
import ProductListItem from '@/components/manage/products/ProductListItem.vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import EditProductModal from '@/components/manage/products/EditProductModal.vue';
import { useProductsQuery } from '@/composables/queries';

//#region list
const {
  data: products,
  isFetching: listFetching,
  isLoading: listLoading,
  isError: listFailed,
  isSuccess: listSuccess,
  error: listError,
} = useProductsQuery();
const loading = computed(() => listLoading.value || listFetching.value);
const loadingLabel = computed(() => {
  if (listLoading.value) return 'Gerechten laden';
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
