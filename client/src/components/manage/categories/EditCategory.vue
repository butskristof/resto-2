<template>
  <BaseModal :show="show">
    <template #header>Edit category</template>
    <template #body>
      <form>
        <div class="form-group">
          <label>
            Name
            <input type="text" />
          </label>
        </div>
        <div class="actions">
          <div class="validation-results"></div>
          <button type="submit">save</button>
        </div>
      </form>
    </template>
  </BaseModal>
</template>

<script>
import BaseModal from '@/components/common/BaseModal.vue';
export default {
  name: 'EditCategory',
  components: { BaseModal },
  props: {
    show: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    tryClose() {
      console.log('try close');
    },
  },
};
</script>

<style scoped lang="scss">
.actions {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
</style>
