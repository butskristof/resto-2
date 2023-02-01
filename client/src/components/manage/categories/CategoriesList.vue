<template>
  <div v-if="isLoading">Loading...</div>
  <div v-else-if="isFetching">Refreshing...</div>
  <div v-else-if="isError">Error: {{ error.message }}</div>
  <ul v-if="data">
    <li v-for="category in data.results" :key="category.id">
      <span class="category-name" :style="{ backgroundColor: category.color }">
        {{ category.name }}
      </span>
      <button type="button" @click="$emit('edit', category)">edit</button>
      <button type="button" @click="tryDelete(category)">delete</button>
    </li>
  </ul>
</template>

<script>
export default {
  name: 'CategoriesList',
  emits: ['edit'],
};
</script>

<script setup>
import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import CategoriesService from '@/services/resto-api/categories.service';

const queryClient = useQueryClient();
const { isFetching, isLoading, isError, data, error } = useQuery({
  queryKey: QUERY_KEYS.CATEGORIES,
  queryFn: () => CategoriesService.get(),
  // select: (r) => r.data,
});

const {
  isLoading: deleteLoading,
  isError: deleteHasError,
  error: deleteError,
  mutate,
} = useMutation({
  mutationFn: (categoryId) => CategoriesService.delete(categoryId),
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.CATEGORIES });
  },
});

const tryDelete = (category) => {
  if (
    confirm(
      `Are you sure you want to delete category with name ${category.name}?`,
    )
  )
    mutate(category.id);
};
</script>

<style scoped></style>
