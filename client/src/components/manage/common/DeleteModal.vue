<template>
  <BaseModal>
    <template #body>
      <div class="body">
        <div>
          {{ capitalize(entity) }}
          <strong>{{ name }}</strong> verwijderen?
        </div>
        <div class="extra-info">
          <slot name="extra-info"></slot>
        </div>
      </div>
    </template>

    <template #footer>
      <div class="footer">
        <div class="left">
          <LoadingIndicator v-if="isLoading"
            >{{ capitalize(entity) }} verwijderen</LoadingIndicator
          >
          <div v-if="isError">
            Er ging iets mis tijdens het verwijderen, probeer later opnieuw.
          </div>
        </div>
        <div class="right">
          <button type="button" class="btn-icon" @click="emit('close')">
            <i class="icon-x"></i> Annuleren
          </button>

          <button
            type="button"
            class="btn-danger btn-icon"
            @click="emit('delete')"
          >
            <i class="icon-trash"></i> Verwijderen
          </button>
        </div>
      </div>
    </template>
  </BaseModal>
</template>

<script setup>
import BaseModal from '@/components/common/BaseModal.vue';
import { capitalize } from '@/utilities/filters';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';

const emit = defineEmits(['close', 'delete']);
defineProps({
  entity: {
    type: String,
    required: true,
  },
  name: {
    type: String,
    required: true,
  },
  isLoading: {
    type: Boolean,
    default: false,
  },
  isError: {
    type: Boolean,
    default: false,
  },
});
</script>

<style scoped lang="scss">
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/utilities/_typography.scss';
@import '@/styles/ui/_layout.scss';

.body {
  margin-top: $box-padding;

  .extra-info {
    margin-top: calc($box-padding / 2);
    @include extra-info-text;
  }
}

.footer {
  margin-top: calc($box-padding * 2);

  @include flex-row-space-between;
  gap: $box-padding;

  .right {
    flex-shrink: 0;

    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
