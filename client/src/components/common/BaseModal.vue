<template>
  <Teleport to="body">
    <div class="modal-mask" v-if="show">
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
  emits: ['close'],
  props: {
    show: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
}

.modal-container {
  max-width: calc(0.75 * $max-body-width);
  width: 95%;
  margin: auto;
  padding: 1rem;
  background-color: #fff;
  border-radius: $box-border-radius;
  //box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
}
</style>
