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
  color: $white;
  background-color: $primary-green;

  &:hover:enabled {
    background-color: darken($primary-green, 8%);
  }

  &:disabled {
    background-color: lighten($primary-green, 20%);
    color: darken($white, 2%);
  }
}

.red {
  color: $white;
  background-color: $primary-red;

  &:hover:enabled {
    background-color: darken($primary-red, 8%);
  }

  &:disabled {
    background-color: lighten($primary-red, 20%);
    color: darken($white, 15%);
  }
}
</style>
