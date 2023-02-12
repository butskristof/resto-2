<template>
  <BaseModal>
    <template #header>
      <div class="header">
        <div class="left">
          <h3>{{ headerText }}</h3>
        </div>

        <div class="right">
          <button type="button" @click="emit('close')">
            <i class="icon-x"></i>
          </button>
        </div>
      </div>
    </template>

    <template #body>
      <slot name="body"></slot>
    </template>
  </BaseModal>
</template>

<script setup>
import BaseModal from '@/components/common/BaseModal.vue';
import { computed } from 'vue';
import { capitalize } from '@/utilities/filters';

const emit = defineEmits(['close']);
const props = defineProps({
  entity: {
    type: String,
    required: true,
  },
  isEdit: {
    type: Boolean,
    default: false,
  },
});

//#region UI
const headerText = computed(() => {
  return `${capitalize(props.entity)} ${
    props.isEdit ? 'bewerken' : 'aanmaken'
  }`;
});
//#endregion
</script>

<style scoped lang="scss">
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/ui/_layout.scss';

.header {
  margin-bottom: $box-padding;

  @include flex-row-space-between;
  align-items: center;

  h3 {
    margin: auto;
  }
}
</style>
