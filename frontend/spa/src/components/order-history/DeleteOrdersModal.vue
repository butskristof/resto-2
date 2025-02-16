<template>
  <DeleteModal
    name=""
    entity="Bestellingen"
    :is-loading="isLoading"
    :is-error="isError"
    @close="emit('close')"
    @delete="triggerMutation"
  />
</template>

<script setup>
import DeleteModal from '@/components/manage/common/DeleteModal.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import OrdersService from '@/services/resto-api/orders.service.js';
import { QUERY_KEYS } from '@/utilities/constants.js';
import { useToast } from 'vue-toastification';

const toast = useToast();

const emit = defineEmits(['close']);
const queryClient = useQueryClient();
const {
  isLoading,
  isError,
  mutate: triggerMutation,
} = useMutation({
  mutationFn: () => OrdersService.delete(),
  onSuccess: () => {
    toast.success('Bestellingen verwijderd');
    queryClient.invalidateQueries({
      queryKey: QUERY_KEYS.ORDERS,
    });
    emit('close');
  },
});
</script>

<style scoped lang="scss"></style>
