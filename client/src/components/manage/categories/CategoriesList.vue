<template>
  <div v-if="isLoading">Loading...</div>
  <div v-else-if="isError">Error: {{ error.message }}</div>
  <ul v-else>
    <li v-for="category in data.results" :key="category.id">
      {{ category.name }}
    </li>
  </ul>
</template>

<script>
export default {
  name: 'CategoriesList',
};
</script>

<script setup>
import { useQuery } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import CategoriesService from '@/services/resto-api/categories.service';

const { isLoading, isError, data, error } = useQuery({
  queryKey: QUERY_KEYS.CATEGORIES,
  queryFn: CategoriesService.get,
});
</script>

<style scoped></style>
