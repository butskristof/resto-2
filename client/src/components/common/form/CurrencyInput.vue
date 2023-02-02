<template>
  <div class="currency-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>

      <span class="input-errors">
        <input
          type="number"
          min="0"
          v-model.number="model"
          :class="{ invalid: hasErrors }"
        />
        <div class="errors" v-if="hasErrors">
          <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
        </div>
      </span>
    </label>
  </div>
</template>

<script setup>
import { computed } from 'vue';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  modelValue: {
    type: [String, Number], // TODO
  },
});
const model = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', (value = Number(value))),
});
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.currency-input {
  @include form-input;
}
</style>
