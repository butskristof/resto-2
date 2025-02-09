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
@use '@/styles/ui/_layout.scss';
@use '@/styles/utilities/_padding-margin.scss';

.header {
  @include layout.flex-row-space-between;
  align-items: center;
  margin-bottom: padding-margin.$box-padding;
}
</style>
