<template>
  <GenericFormInput
    :errors="errors"
    :nested-input="true"
  >
    <template #label>
      <slot name="label"></slot>
    </template>

    <template #input>
      <ColorInput
        v-model="model"
        :invalid="hasErrors"
      />
    </template>
  </GenericFormInput>
</template>

<script setup>
import { computed } from 'vue';
import { useVModel } from '@vueuse/core';
import GenericFormInput from '@/components/common/form/rows/GenericFormInput.vue';
import ColorInput from '@/components/common/form/inputs/ColorInput.vue';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  modelValue: {
    type: String,
    default: '#000',
  },
});

const model = useVModel(props, 'modelValue', emit);
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>
