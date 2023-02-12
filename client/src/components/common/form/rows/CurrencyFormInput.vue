<template>
  <div class="currency-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>

      <span class="input-errors">
        <input ref="inputRef" :class="{ invalid: hasErrors }" type="text" />
        <div v-if="hasErrors" class="errors">
          <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
        </div>
      </span>
    </label>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import { useCurrencyInput } from 'vue-currency-input';
// https://dm4t2.github.io/vue-currency-input/playground.html

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
    default: () => ({
      locale: 'nl-BE',
      currency: 'EUR',
      currencyDisplay: 'symbol',
      valueRange: {
        min: 0,
      },
      hideCurrencySymbolOnFocus: false,
      hideGroupingSeparatorOnFocus: false,
      hideNegligibleDecimalDigitsOnFocus: false,
      autoDecimalDigits: false,
      useGrouping: true,
      accountingSign: false,
    }),
  },
});
const { inputRef } = useCurrencyInput(props.options);
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.currency-input {
  @include form-row;
}
</style>
