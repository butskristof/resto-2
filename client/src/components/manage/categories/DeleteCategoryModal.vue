<template>
  <DeleteModal
    @close="emit('close')"
    @delete="triggerMutation"
    entity="categorie"
    :name="category.name"
    :is-loading="isLoading"
    :is-error="isError"
  >
    <template #extra-info>
      De categorie zal enkel verwijderd kunnen worden als er geen producten meer
      aan gekoppeld zijn.
    </template>
  </DeleteModal>
</template>

<script setup>
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import CategoriesService from '@/services/resto-api/categories.service';
import { QUERY_KEYS } from '@/utilities/constants';
import DeleteModal from '@/components/manage/common/DeleteModal.vue';

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
  mutate: triggerMutation,
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
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.body {
  margin-top: $box-padding;

  .extra-info {
    @include extra-info-text;
  }
}

.footer {
  margin-top: calc($box-padding * 2);

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
