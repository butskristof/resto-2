<template>
  <div class="load-next-page">
    <button type="button" @click="emit('load-next-page')">
      Laad meer {{ entity }}
    </button>
  </div>
</template>

<script setup>
const emit = defineEmits(['load-next-page']);
defineProps({
  entity: {
    type: String,
    required: true,
  },
});
</script>

<style scoped lang="scss">
@use '@/styles/utilities/_padding-margin.scss';

.load-next-page {
  display: flex;
  justify-content: center;
  margin-top: padding-margin.$box-padding;
}
</style>
