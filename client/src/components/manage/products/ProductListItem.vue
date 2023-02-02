<template>
  <div class="product">{{ product.name }}</div>
</template>

<script setup>
defineProps({
  product: {
    type: Object,
    required: true,
  },
});
</script>

<style scoped></style>
