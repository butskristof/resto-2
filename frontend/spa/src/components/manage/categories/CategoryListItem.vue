<template>
  <div class="category">
    <div class="color" :style="colorBlockStyle"></div>
    <div class="name">{{ category.name }}</div>

    <div class="actions">
      <button
        type="button"
        class="btn-blue btn-icon"
        @click="emit('edit', category)"
      >
        <i class="icon-edit"></i> Bewerken
      </button>
      <button
        type="button"
        class="btn-danger btn-icon"
        @click="emit('delete', category)"
      >
        <i class="icon-trash"></i> Verwijderen
      </button>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue';

const emit = defineEmits(['edit', 'delete']);
const props = defineProps({
  category: {
    type: Object,
    required: true,
  },
});
const colorBlockStyle = computed(() => ({
  'background-color': props.category.color,
}));
</script>

<style scoped lang="scss">
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/utilities/_general.scss';
@use '@/styles/ui/_layout.scss';
@use '@/styles/manage/_common.scss';

.category {
  min-height: common.$list-row-height;
  @include layout.flex-row;
  align-items: center;
  gap: padding-margin.$box-padding;
  @include general.striped-rows;

  .color {
    @include general.category-color-block;
  }

  .name {
    flex-grow: 1;
  }

  .actions {
    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
