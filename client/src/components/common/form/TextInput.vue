<template>
  <div class="text-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>
      <span class="input-errors">
        <input
          v-model.trim="model"
          type="text"
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
import { useVModel } from '@vueuse/core';

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

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.text-input {
  @include form-input;
}
</style>
