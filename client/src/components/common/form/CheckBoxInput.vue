<template>
  <div class="checkbox-input">
    <label>
      <span class="label">
        <slot name="label"></slot>
      </span>
    </label>
    <div class="input-errors">
      <CheckboxToggle v-model="model" />
      <div class="errors" v-if="hasErrors">
        <div v-for="(error, i) in errors" :key="i">{{ error }}</div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import CheckboxToggle from '@/components/common/form/CheckboxToggle.vue';

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
const model = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value),
});
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

  .checkbox {
    input[type='checkbox'] {
      height: 0;
      width: 0;
      visibility: hidden;
    }

    label {
      cursor: pointer;
      text-indent: -9999px;
      width: 200px;
      height: 100px;
      background: grey;
      display: block;
      border-radius: 100px;
      position: relative;
    }

    label:after {
      content: '';
      position: absolute;
      top: 5px;
      left: 5px;
      width: 90px;
      height: 90px;
      background: #fff;
      border-radius: 90px;
      transition: 0.3s;
    }

    input:checked + label {
      background: #bada55;
    }

    input:checked + label:after {
      left: calc(100% - 5px);
      transform: translateX(-100%);
    }

    label:active:after {
      width: 130px;
    }
  }
}
</style>
