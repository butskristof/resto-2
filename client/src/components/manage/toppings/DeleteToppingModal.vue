<template>
  <DeleteModal
    entity="topping"
    :name="topping.name"
    :is-loading="isLoading"
    :is-error="isError"
    @close="emit('close')"
    @delete="triggerMutation"
  >
    <template #extra-info>
      De topping zal enkel verwijderd kunnen worden als er geen producten meer
      aan gekoppeld zijn.
    </template>
  </DeleteModal>
</template>

<script setup>
import { computed } from 'vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import ToppingsService from '@/services/resto-api/toppings.service';
import { QUERY_KEYS } from '@/utilities/constants';
import DeleteModal from '@/components/manage/common/DeleteModal.vue';

const emit = defineEmits(['close']);
const props = defineProps({
  topping: {
    type: Object,
    required: true,
  },
});
const topping = computed(() => props.topping);

//#region query
const queryClient = useQueryClient();
const {
  isLoading,
  isError,
  mutate: triggerMutation,
} = useMutation({
  mutationFn: () => ToppingsService.delete(topping.value.id),
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.TOPPINGS });
    emit('close');
  },
});
//#endregion
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.body {
  margin-top: $box-padding;

  .extra-info {
    font-style: italic;
    font-size: 80%;
    color: lighten($body-text-color, 30%);
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
