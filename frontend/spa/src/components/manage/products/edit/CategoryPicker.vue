<template>
  <div class="category-picker">
    <VueMultiselect
      v-model="model"
      :loading="categoriesLoading"
      :options="categories"
      :disabled="disabled"
      track-by="id"
      label="name"
      :multiple="multiple"
      :searchable="true"
      :close-on-select="true"
      :show-labels="false"
      :placeholder="placeholder"
    />
    <div v-if="categoriesError" class="error">
      Categorieën konden niet worden ingeladen, probeer het later opnieuw.
    </div>
  </div>
</template>

<script setup>
import VueMultiselect from 'vue-multiselect';
import { computed } from 'vue';
import { useCategoriesQuery } from '@/composables/queries';
import { useVModel } from '@vueuse/core';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  modelValue: {
    type: Object,
    default: null,
  },
  multiple: {
    type: Boolean,
    default: false,
  },
});
const model = useVModel(props, 'modelValue', emit);

const placeholder = computed(() => {
  let placeholder = 'Kies categorie';
  if (props.multiple) placeholder += 'ën';
  return placeholder;
});

const {
  categories,
  isLoading: categoriesLoading,
  isError: categoriesError,
} = useCategoriesQuery(true);
const disabled = computed(
  () => categoriesLoading.value || categoriesError.value,
);
</script>

<style scoped lang="scss">
@import '@/styles/utilities/_typography.scss';

.error {
  @include error-text;
  margin-top: 0.5rem;
}
</style>
