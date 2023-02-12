<template>
  <div class="number-input">
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

const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  modelValue: Number,
  options: {
    locale: 'nl-NL',
    currency: 'EUR',
    currencyDisplay: 'hidden',
    hideCurrencySymbolOnFocus: true,
    hideGroupingSeparatorOnFocus: false,
    hideNegligibleDecimalDigitsOnFocus: false,
    autoDecimalDigits: false,
    useGrouping: true,
    accountingSign: false,
  },
});
const { inputRef } = useCurrencyInput(props.options);
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.number-input {
  @include form-row;
}
</style>
