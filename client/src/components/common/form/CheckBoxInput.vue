<template>
  <div class="checkbox-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>
    </label>
    <div class="input-errors">
      <CheckboxToggle v-model="model" />
      <div v-if="hasErrors" class="errors">
        <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import CheckboxToggle from '@/components/common/form/CheckboxToggle.vue';
import { useVModel } from '@vueuse/core';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  errors: {
    type: Array,
    required: false,
    default: () => [],
  },
  modelValue: {
    type: Boolean,
    default: false,
  },
});
const model = useVModel(props, 'modelValue', emit);
const hasErrors = computed(() => props.errors && props.errors.length > 0);
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.checkbox-input {
  @include form-input;

  display: flex;
  flex-direction: row;

  label {
    flex-grow: 1;
  }

  .input-errors {
    input {
      width: initial;
    }
  }
}
</style>
