import { computed } from 'vue';

export function useEditModalActionElements(isEdit) {
  const actionLabel = computed(() => (isEdit.value ? 'opslaan' : 'aanmaken'));
  const actionIcon = computed(() => 'icon-' + (isEdit.value ? 'save' : 'plus'));

  return {
    actionLabel,
    actionIcon,
  };
}
