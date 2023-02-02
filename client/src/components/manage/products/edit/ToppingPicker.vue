<template>
  <div class="topping-picker">
    <VueMultiselect
      v-model="model"
      :options="toppings?.results ?? []"
      track-by="id"
      label="name"
      :multiple="multiple"
      :searchable="true"
      :close-on-select="false"
      :show-labels="false"
      :placeholder="placeholder"
    />
  </div>
</template>

<script setup>
import VueMultiselect from 'vue-multiselect';
import { computed } from 'vue';
import { useToppingsQuery } from '@/composables/queries';

const multiple = true;

const placeholder = computed(() => {
  let placeholder = 'Kies topping';
  if (multiple) placeholder += 's';
  return placeholder;
});

const { data: toppings } = useToppingsQuery();

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

<style scoped></style>
