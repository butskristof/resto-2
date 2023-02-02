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
          <i class="icon-plus"></i> Topping toevoegen
        </button>
      </div>
    </div>

    <div class="list">
      <div v-if="listSuccess">
        <ToppingListItem
          v-for="topping in toppings.results"
          :key="topping.id"
          :topping="topping"
          @edit="openEditModal"
          @delete="openDeleteModal"
        />
      </div>
    </div>

    <DeleteToppingModal
      v-if="showDeleteModal"
      @close="closeDeleteModal"
      :topping="toppingToDelete"
    />
  </div>
</template>

<script setup>
import { useQuery } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import ToppingsService from '@/services/resto-api/toppings.service';
import { computed, ref } from 'vue';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import ToppingListItem from '@/components/manage/toppings/ToppingListItem.vue';
import DeleteToppingModal from '@/components/manage/toppings/DeleteToppingModal.vue';

//#region list
const {
  data: toppings,
  isFetching: listFetching,
  isLoading: listLoading,
  isError: listFailed,
  isSuccess: listSuccess,
  error: listError,
} = useQuery({
  queryKey: QUERY_KEYS.TOPPINGS,
  queryFn: async () => (await ToppingsService.get()).data,
});
const loading = computed(() => listLoading.value || listFetching.value);
const loadingLabel = computed(() => {
  if (listLoading.value) return 'Toppings laden';
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

<style scoped lang="scss">
.header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}
</style>