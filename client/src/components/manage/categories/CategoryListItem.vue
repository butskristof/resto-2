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
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/utilities/_general.scss';
@import '@/styles/ui/_layout.scss';
@import '@/styles/manage/_common.scss';

.category {
  min-height: $list-row-height;
  @include flex-row;
  align-items: center;
  gap: $box-padding;
  @include striped-rows;

  .color {
    @include category-color-block;
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
