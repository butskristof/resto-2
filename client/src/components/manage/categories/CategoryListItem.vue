<template>
  <div class="category">
    <div class="name">{{ category.name }}</div>
    <div class="color" :style="colorBlockStyle"></div>
    <div class="actions">
      <IconButton @click="$emit('edit', category)">bewerken</IconButton>
      <IconButton @click="$emit('delete', category)">verwijderen</IconButton>
    </div>
  </div>
</template>

<script>
import IconButton from '@/components/common/IconButton.vue';

export default {
  name: 'CategoryListItem',
  components: { IconButton },
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
  height: 2rem;
  padding: 0rem 1rem;
  @include striped-rows;

  .name {
    flex-grow: 1;
  }

  .color {
    flex-basis: 100px;
    height: 100%;
  }

  .actions {
    margin-left: 1rem;

    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
