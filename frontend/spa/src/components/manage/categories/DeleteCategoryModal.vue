<template>
  <DeleteModal
    entity="categorie"
    :name="category.name"
    :is-loading="isLoading"
    :is-error="isError"
    @close="emit('close')"
    @delete="triggerMutation"
  >
    <template #extra-info>
      De categorie zal enkel verwijderd kunnen worden als er geen producten meer aan gekoppeld zijn.
    </template>
  </DeleteModal>
</template>

<script setup>
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import CategoriesService from '@/services/resto-api/categories.service';
import { QUERY_KEYS } from '@/utilities/constants';
import DeleteModal from '@/components/manage/common/DeleteModal.vue';
import { useToast } from 'vue-toastification';

const toast = useToast();

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
  mutate: triggerMutation,
} = useMutation({
  mutationFn: () => CategoriesService.delete(props.category.id),
  onSuccess: () => {
    toast.success('Categorie verwijderd');
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.CATEGORIES });
    emit('close');
  },
});

//#endregion
</script>

<style scoped lang="scss"></style>
