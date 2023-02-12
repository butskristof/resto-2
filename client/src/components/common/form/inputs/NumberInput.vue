<template>
  <input ref="inputRef" :class="{ invalid: invalid }" type="text" />
</template>

<script setup>
import { useCurrencyInput } from 'vue-currency-input';
import { DEFAULT_NUMBER_INPUT_OPTIONS } from '@/utilities/currencies';

// https://dm4t2.github.io/vue-currency-input/playground.html
const props = defineProps({
  invalid: {
    type: Boolean,
    default: false,
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
const { inputRef } = useCurrencyInput(props.options);
</script>

<style scoped lang="scss">
@import '@/styles/elements/_forms.scss';

input {
  @include input;
}
</style>
