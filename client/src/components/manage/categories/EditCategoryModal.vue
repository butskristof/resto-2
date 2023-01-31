<template>
  <BaseModal>
    <template #header>
      <div class="header">
        <div class="left">
          <h3>Categorie bewerken</h3>
        </div>
        <div class="right">
          <button type="button" @click="$emit('close')">x</button>
        </div>
      </div>
    </template>
    <template #body>
      <form>
        <TextInput />
        <ColorInput />
        <div class="form-actions">
          <div class="left"></div>
          <div class="right">submit</div>
        </div>
      </form>
    </template>
  </BaseModal>
</template>

<script>
import BaseModal from '@/components/common/BaseModal.vue';
import TextInput from '@/components/common/form/TextInput.vue';
import ColorInput from '@/components/common/form/ColorInput.vue';

export default {
  name: 'EditCategoryModal',
  components: { ColorInput, TextInput, BaseModal },
  emits: ['close'],
};
</script>

<style scoped lang="scss">
.header,
.form-actions {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
</style>
