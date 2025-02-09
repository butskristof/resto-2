<template>
  <GenericFormInput
    :errors="errors"
    :nested-input="true"
  >
    <template #label>
      <slot name="label"></slot>
    </template>

    <template #input>
      <TextInput
        v-model="model"
        :invalid="hasErrors"
      />
    </template>
  </GenericFormInput>
</template>

<script setup>
import { computed } from 'vue';
import { useVModel } from '@vueuse/core';
import TextInput from '@/components/common/form/inputs/TextInput.vue';
import GenericFormInput from '@/components/common/form/rows/GenericFormInput.vue';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  modelValue: {
    type: String,
    default: '',
  },
});

const model = useVModel(props, 'modelValue', emit);
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss"></style>
