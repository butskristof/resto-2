<template>
  <BaseModal>
    <template #header>
      <div class="header">
        <div class="left">
          <h3>Categorie verwijderen</h3>
        </div>
        <div class="right">
          <button type="button" @click="emit('close')">x</button>
        </div>
      </div>
    </template>

    <template #body> {{ category.name }} verwijderen? </template>

    <template #footer>
      <div class="footer">
        <div class="left">
          <div v-if="isLoading">Deleting...</div>
          <div v-if="isError">
            Er ging iets mis tijdens het verwijderen, probeer later opnieuw.
          </div>
        </div>
        <div class="right">
          <button type="button" @click="emit('close')">annuleren</button>
          <button type="button" @click="mutate">verwijderen</button>
        </div>
      </div>
    </template>
  </BaseModal>
</template>

<script setup>
import BaseModal from '@/components/common/BaseModal.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import CategoriesService from '@/services/resto-api/categories.service';
import { QUERY_KEYS } from '@/utilities/constants';

const emit = defineEmits(['close']);
const props = defineProps({
  category: {
    type: Object,
    default: null,
  },
});

//#region query

const queryClient = useQueryClient();
const {
  isLoading,
  isError,
  // error,
  // isSuccess,
  mutate,
} = useMutation({
  mutationFn: () => CategoriesService.delete(props.category.id),
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.CATEGORIES });
    emit('close');
  },
});

//#endregion
</script>

<style scoped lang="scss">
.header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}

.footer {
  margin-top: 1rem;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
  gap: 1rem;

  .right {
    flex-shrink: 0;

    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
