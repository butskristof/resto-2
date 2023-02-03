<template>
  <div class="number-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>

      <span class="input-errors">
        <input
          v-model.number="model"
          type="number"
          :class="{ invalid: hasErrors }"
        />
        <div v-if="hasErrors" class="errors">
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
    type: [Number],
  },
});
const model = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', (value = Number(value))),
});
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.number-input {
  @include form-input;
}
</style>
