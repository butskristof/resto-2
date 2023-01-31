<template>
  <div class="text-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>
      <span class="input-errors">
        <input
          type="text"
          v-model.trim="model"
          :class="{ invalid: hasErrors }"
        />
        <div class="errors" v-if="hasErrors">
          <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
        </div>
      </span>
    </label>
  </div>
</template>

<script>
import { computed } from 'vue';

export default {
  name: 'TextInput',
  props: {
    errors: {
      type: Array,
      required: false,
      default: () => [],
    },
    modelValue: {
      type: [String],
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
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.text-input {
  @include form-input;
}
</style>
