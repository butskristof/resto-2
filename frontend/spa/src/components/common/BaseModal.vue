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
@use '@/styles/ui/_layout.scss';
@use '@/styles/utilities/_padding-margin.scss';

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
  max-width: calc(0.6 * #{layout.$app-width});
  width: 100%;
  margin: auto calc(2 * #{padding-margin.$box-padding});
  padding: padding-margin.$box-padding;
  background-color: white;
  border-radius: padding-margin.$box-border-radius;
}
</style>
