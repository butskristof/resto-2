<template>
  <DeleteModal
    @close="emit('close')"
    @delete="triggerMutation"
    entity="gerecht"
    :name="product.name"
    :is-loading="isLoading"
    :is-error="isError"
  />
</template>

<script setup>
import DeleteModal from '@/components/manage/common/DeleteModal.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import ProductsService from '@/services/resto-api/products.service';
import { QUERY_KEYS } from '@/utilities/constants';

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
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.PRODUCTS });
    emit('close');
  },
});
//#endregion
</script>

<style scoped lang="scss"></style>
