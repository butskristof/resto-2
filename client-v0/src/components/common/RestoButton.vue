<template>
  <button type="button" :class="theme" :disabled="disabled">
    <slot></slot>
  </button>
</template>

<script>
export default {
  name: 'RestoButton',
  props: {
    theme: {
      type: String,
      required: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_buttons.scss';

button {
  @include reset-button;
  @include styled-button-base;
}

.green {
  @include button-green;
}

.red {
  @include button-red;
}
</style>
