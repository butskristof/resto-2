<template>
  <div class="topping">
    <div class="name">{{ topping.name }}</div>

    <div class="price">{{ formatCurrency(topping.price) }}</div>

    <div class="actions">
      <button
        type="button"
        class="btn-blue btn-icon"
        @click="emit('edit', topping)"
      >
        <i class="icon-edit"></i> Bewerken
      </button>
      <button
        type="button"
        class="btn-danger btn-icon"
        @click="emit('delete', topping)"
      >
        <i class="icon-trash"></i> Verwijderen
      </button>
    </div>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utilities/filters';
import { computed } from 'vue';

const emit = defineEmits(['edit', 'delete']);
const props = defineProps({
  topping: {
    type: Object,
    required: true,
  },
});
const topping = computed(() => props.topping);
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.topping {
  width: 100%;
  height: 2.5rem;
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: $box-padding;
  @include striped-rows;

  padding-left: $box-padding;

  .name {
    flex-grow: 1;
  }

  .actions {
    button {
    }
    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
