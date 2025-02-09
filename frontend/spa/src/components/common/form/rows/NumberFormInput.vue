<template>
  <GenericFormInput
    :errors="errors"
    :nested-input="true"
  >
    <template #label>
      <slot name="label"></slot>
    </template>

    <template #input>
      <NumberInput
        v-model="model"
        :invalid="hasErrors"
        :options="options"
      />
    </template>
  </GenericFormInput>
</template>

<script setup>
import { computed } from 'vue';
import GenericFormInput from '@/components/common/form/rows/GenericFormInput.vue';
import NumberInput from '@/components/common/form/inputs/NumberInput.vue';
import { useVModel } from '@vueuse/core';
import { DEFAULT_NUMBER_INPUT_OPTIONS } from '@/utilities/currencies';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  modelValue: {
    type: Number,
    default: null,
  },
  options: {
    type: Object,
    default: () => DEFAULT_NUMBER_INPUT_OPTIONS,
  },
});
const model = useVModel(props, 'modelValue', emit);
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss"></style>
