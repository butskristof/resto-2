<template>
  <div class="input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>

      <div v-if="nestedInput" class="input-errors">
        <slot name="input"></slot>
        <div v-if="hasErrors" class="errors">
          <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
        </div>
      </div>
    </label>

    <div v-if="!nestedInput" class="input-errors">
      <slot name="input"></slot>
      <div v-if="hasErrors" class="errors">
        <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue';

const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  nestedInput: {
    type: Boolean,
    default: false,
  },
});
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/elements/_forms.scss';

.input {
  @include form-row;
  @include flex-row;

  label {
    flex-grow: 1;
  }
}
</style>
