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
          <LoadingIndicator v-if="isLoading">{{ capitalize(entity) }} verwijderen</LoadingIndicator>
          <div v-if="isError">Er ging iets mis tijdens het verwijderen, probeer later opnieuw.</div>
        </div>
        <div class="right">
          <button
            type="button"
            class="btn-icon"
            @click="emit('close')"
          >
            <i class="icon-x" /> Annuleren
          </button>

          <button
            type="button"
            class="btn-danger btn-icon"
            @click="emit('delete')"
          >
            <i class="icon-trash" /> Verwijderen
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
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/utilities/_typography.scss';
@use '@/styles/ui/_layout.scss';

.body {
  margin-top: padding-margin.$box-padding;

  .extra-info {
    margin-top: calc(#{padding-margin.$box-padding} / 2);
    @include typography.extra-info-text;
  }
}

.footer {
  margin-top: calc(#{padding-margin.$box-padding} * 2);

  @include layout.flex-row-space-between;
  gap: padding-margin.$box-padding;

  .right {
    flex-shrink: 0;

    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
