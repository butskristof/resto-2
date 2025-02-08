<template>
  <Teleport to="body">
    <div class="modal-mask">
      <div class="modal-container">
        <div class="modal-header">
          <slot name="header"></slot>
        </div>

        <div class="modal-body">
          <slot name="body"></slot>
        </div>

        <div class="modal-footer">
          <slot name="footer"></slot>
        </div>
      </div>
    </div>
  </Teleport>
</template>

<script>
export default {
  name: 'BaseModal',
  props: {
    show: {
      type: Boolean,
      default: false,
    },
  },
  emits: ['close'],
};
</script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_padding-margin.scss';

.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-container {
  max-width: calc(0.6 * $app-width);
  width: 100%;
  margin: auto calc(2 * $box-padding);
  padding: $box-padding;
  background-color: white;
  border-radius: $box-border-radius;
}
</style>
