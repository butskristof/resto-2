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
      De topping zal enkel verwijderd kunnen worden als er geen producten meer aan gekoppeld zijn.
    </template>
  </DeleteModal>
</template>

<script setup>
import { computed } from 'vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import ToppingsService from '@/services/resto-api/toppings.service';
import { QUERY_KEYS } from '@/utilities/constants';
import DeleteModal from '@/components/manage/common/DeleteModal.vue';
import { useToast } from 'vue-toastification';

const toast = useToast();

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
    toast.success('Topping verwijderd');
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.TOPPINGS });
    emit('close');
  },
});
//#endregion
</script>

<style scoped lang="scss"></style>
