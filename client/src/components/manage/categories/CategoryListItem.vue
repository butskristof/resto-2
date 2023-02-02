<template>
  <div class="category">
    <div class="color" :style="colorBlockStyle"></div>
    <div class="name">{{ category.name }}</div>
    <div class="actions">
      <button
        type="button"
        class="btn-blue btn-icon"
        @click="$emit('edit', category)"
      >
        <i class="icon-edit"></i> Bewerken
      </button>
      <button
        type="button"
        class="btn-danger btn-icon"
        @click="$emit('delete', category)"
      >
        <i class="icon-trash"></i> Verwijderen
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CategoryListItem',
  emits: ['edit', 'delete'],
  props: {
    category: {
      type: Object,
      required: true,
    },
  },
  computed: {
    colorBlockStyle() {
      return {
        'background-color': this.category.color,
      };
    },
  },
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

.category {
  width: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
  height: 2.5rem;
  @include striped-rows;

  .color {
    flex-basis: 100px;
    margin-right: $box-padding;
    height: 100%;
  }

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
