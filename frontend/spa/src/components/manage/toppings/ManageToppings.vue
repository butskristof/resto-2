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
          <i class="icon-plus" /> Topping toevoegen
        </button>
      </div>
    </template>

    <template #list>
      <div v-if="listSuccess">
        <ToppingListItem
          v-for="topping in toppings"
          :key="topping.id"
          :topping="topping"
          @edit="openEditModal"
          @delete="openDeleteModal"
        />
        <LoadNextPage
          v-if="listHasNextPage"
          entity="toppings"
          @load-next-page="listFetchNextPage"
        />
      </div>
    </template>

    <template #modals>
      <EditToppingModal
        v-if="showEditModal"
        :topping="toppingToEdit"
        @close="closeEditModal"
      />
      <DeleteToppingModal
        v-if="showDeleteModal"
        :topping="toppingToDelete"
        @close="closeDeleteModal"
      />
    </template>
  </ManageBase>
</template>

<script setup>
import { computed, ref } from 'vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import ToppingListItem from '@/components/manage/toppings/ToppingListItem.vue';
import DeleteToppingModal from '@/components/manage/toppings/DeleteToppingModal.vue';
import EditToppingModal from '@/components/manage/toppings/EditToppingModal.vue';
import { useToppingsQuery } from '@/composables/queries';
import LoadNextPage from '@/components/common/LoadNextPage.vue';
import ManageBase from '@/components/manage/common/ManageBase.vue';

//#region list
const {
  toppings,

  fetchNextPage: listFetchNextPage,
  hasNextPage: listHasNextPage,

  isLoading: listLoading,
  isFetching: listFetching,
  isFetchingNextPage: listFetchingNextPage,

  isSuccess: listSuccess,
  isError: listFailed,
  error: listError,
} = useToppingsQuery();
const loading = computed(
  () => listLoading.value || listFetching.value || listFetchingNextPage.value,
);
const loadingLabel = computed(() => {
  if (listLoading.value || listFetchingNextPage.value) return 'Toppings laden';
  else if (listFetching.value) return 'Toppings bijwerken';
  return '';
});
//#endregion

//#region create & update model
const showEditModal = ref(false);
const toppingToEdit = ref(null);
const openEditModal = (topping) => {
  toppingToEdit.value = topping;
  showEditModal.value = true;
};
const closeEditModal = () => {
  toppingToEdit.value = null;
  showEditModal.value = false;
};
//#endregion

//#region delete
const toppingToDelete = ref(null);
const showDeleteModal = computed(() => toppingToDelete.value != null);
const openDeleteModal = (topping) => {
  toppingToDelete.value = topping;
};
const closeDeleteModal = () => {
  toppingToDelete.value = null;
};
//#endregion
</script>

<style scoped lang="scss"></style>
