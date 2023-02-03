<template>
  <div class="load-next-page">
    <button type="button" @click="emit('load-next-page')">Load more</button>
  </div>
</template>

<script setup>
const emit = defineEmits(['load-next-page']);
</script>

<style scoped lang="scss">
.load-next-page {
  display: flex;
  justify-content: center;
  margin-top: 1rem;
}
</style>
