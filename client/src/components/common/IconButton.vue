<template>
  <button :type="type">
    <span v-if="$slots.icon" class="icon">
      <slot name="icon"></slot>
    </span>
    <span class="label"><slot></slot></span>
  </button>
</template>

<script>
export default {
  props: {
    type: {
      type: String,
      default: 'button',
    },
    label: {
      type: String,
      default: '',
    },
  },
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

button {
  @include reset-button;

  border: 1px solid $body-text-color;

  .icon {
    margin-right: 0.25rem;
  }
}
</style>
