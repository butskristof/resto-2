<template>
  <input v-model="model" type="color" :class="{ invalid: invalid }" />
</template>

<script setup>
import { useVModel } from '@vueuse/core';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  invalid: {
    type: Boolean,
    default: false,
  },
  modelValue: {
    type: String,
    default: '#000',
  },
});
const model = useVModel(props, 'modelValue', emit);
</script>

<style scoped lang="scss">
@import '@/styles/elements/_forms.scss';

input {
  @include input;
  padding: 0.25rem 0.9rem;
  height: 2.5rem;
}
</style>
