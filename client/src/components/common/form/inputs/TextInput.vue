<template>
  <input v-model.trim="model" type="text" :class="{ invalid: invalid }" />
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
    default: '',
  },
});
const model = useVModel(props, 'modelValue', emit);
</script>

<style scoped lang="scss">
@import '@/styles/elements/_forms.scss';

input {
  @include input;
}
</style>
