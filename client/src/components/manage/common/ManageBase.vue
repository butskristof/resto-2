<template>
  <div class="manage">
    <div class="header">
      <slot name="header"></slot>
    </div>

    <div class="list">
      <slot name="list"></slot>
    </div>

    <slot name="modals"></slot>
  </div>
</template>

<script setup></script>

<style scoped lang="scss">
@import '@/styles/ui/_layout.scss';
@import '@/styles/utilities/_padding-margin.scss';

.header {
  @include flex-row-space-between;
  align-items: center;
  margin-bottom: $box-padding;
}
</style>
