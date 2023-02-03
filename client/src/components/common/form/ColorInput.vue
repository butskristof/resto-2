<template>
  <div class="color-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>
      <span class="input-errors">
        <input
          v-model.trim="model"
          type="color"
          :class="{ invalid: hasErrors }"
        />
        <div v-if="hasErrors" class="errors">
          <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
        </div>
      </span>
    </label>
  </div>
</template>

<script>
import { computed } from 'vue';

export default {
  props: {
    errors: {
      type: Array,
      required: false,
      default: () => [],
    },
    modelValue: {
      type: String,
    },
  },
  setup(props, { emit }) {
    // https://vanoneang.github.io/article/v-model-in-vue3.html
    const model = computed({
      get: () => props.modelValue,
      set: (value) => emit('update:modelValue', value),
    });
    const hasErrors = computed(() => props.errors && props.errors.length > 0);
    return { model, hasErrors };
  },
};
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.color-input {
  @include form-input;
}
</style>
