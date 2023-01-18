<template>
  <pre>{{ JSON.stringify(currentOrder, null, 2) }}</pre>
</template>

<script setup>
import { useCurrentOrderStore } from '@/stores/current-order';

const { currentOrder } = useCurrentOrderStore();
</script>

<style scoped></style>
