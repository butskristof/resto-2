<template>
  <div class="input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>
    </label>

    <div class="input-errors">
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
});
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.input {
  @include form-input;
  display: flex;
  flex-direction: row;

  label {
    flex-grow: 1;
  }
}
</style>
