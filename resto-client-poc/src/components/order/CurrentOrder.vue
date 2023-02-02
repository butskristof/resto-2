<template>
  <div class="wrapper">
    <h3>Current order</h3>
    <pre>{{ JSON.stringify(currentOrder, null, 2) }}</pre>
  </div>
</template>

<script setup>
import { useCurrentOrderStore } from '@/stores/current-order';

const { currentOrder } = useCurrentOrderStore();
</script>

<style scoped lang="scss">
.wrapper {
  padding: 0 1rem;

  pre {
    overflow: auto;
  }
}
</style>
