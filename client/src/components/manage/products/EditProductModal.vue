<template>
  <EditModal entity="gerecht" @close="tryClose" :is-edit="isEdit">
    <template #body>
      <div>
        <CategoryPicker />
      </div>
    </template>
  </EditModal>
</template>

<script setup>
import EditModal from '@/components/manage/common/EditModal.vue';
import { computed } from 'vue';
import CategoryPicker from '@/components/manage/products/edit/CategoryPicker.vue';

const emit = defineEmits(['close']);
const props = defineProps({
  product: {
    type: Object,
    default: null,
  },
});
const isEdit = computed(() => props.product != null);

//#region UI
const actionLabel = computed(() => (isEdit.value ? 'opslaan' : 'aanmaken'));
const actionIcon = computed(() => 'icon-' + (isEdit.value ? 'save' : 'plus'));
//#endregion

//#region form
//#endregion

//#region query
//#endregion

const tryClose = (force = false) => {
  let close = true;
  // if (!force && formMeta.value.dirty)
  //   close = confirm(
  //       'There may be unsaved changes, are you sure you want to stop editing?',
  //   );
  if (close) emit('close');
};
</script>

<style scoped lang="scss"></style>
