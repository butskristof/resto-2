<template>
  <GenericFormInput
    :errors="errors"
    :nested-input="false"
  >
    <template #label>
      <slot name="label"></slot>
    </template>

    <template #input>
      <CheckboxInput v-model="model" />
    </template>
  </GenericFormInput>
</template>

<script setup>
import CheckboxInput from '@/components/common/form/inputs/CheckboxInput.vue';
import { useVModel } from '@vueuse/core';
import GenericFormInput from '@/components/common/form/rows/GenericFormInput.vue';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  modelValue: {
    type: Boolean,
    default: false,
  },
});
const model = useVModel(props, 'modelValue', emit);
</script>

<style scoped lang="scss"></style>
