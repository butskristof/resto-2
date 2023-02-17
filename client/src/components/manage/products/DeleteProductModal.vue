<template>
  <DeleteModal
    entity="gerecht"
    :name="product.name"
    :is-loading="isLoading"
    :is-error="isError"
    @close="emit('close')"
    @delete="triggerMutation"
  />
</template>

<script setup>
import DeleteModal from '@/components/manage/common/DeleteModal.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import ProductsService from '@/services/resto-api/products.service';
import { QUERY_KEYS } from '@/utilities/constants';
import { useToast } from 'vue-toastification';

const toast = useToast();

const emit = defineEmits(['close']);
const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});

//#region query
const queryClient = useQueryClient();
const {
  isLoading,
  isError,
  mutate: triggerMutation,
} = useMutation({
  mutationFn: () => ProductsService.delete(props.product.id),
  onSuccess: () => {
    toast.success('Gerecht verwijderd');
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.PRODUCTS });
    emit('close');
  },
});
//#endregion
</script>

<style scoped lang="scss"></style>
