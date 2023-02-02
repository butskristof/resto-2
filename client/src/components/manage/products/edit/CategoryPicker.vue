<template>
  <div class="category-picker">
    <VueMultiselect
      v-model="model"
      :options="categories?.results ?? []"
      track-by="id"
      label="name"
      :searchable="true"
      :close-on-select="true"
      :show-labels="false"
      placeholder="Kies categorie"
    />
  </div>
</template>

<script setup>
import VueMultiselect from 'vue-multiselect';
import { computed } from 'vue';
import { useCategoriesQuery } from '@/composables/queries';

const {
  data: categories,
  isLoading: categoriesLoading,
  isSuccess: categoriesLoaded,
} = useCategoriesQuery();

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  modelValue: {
    type: Object,
    default: null,
  },
});
const model = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value),
});
</script>

<style scoped lang="scss"></style>
