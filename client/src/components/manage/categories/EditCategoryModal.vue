<template>
  <EditModal entity="categorie" :is-edit="isEdit" @close="tryClose">
    <template #body>
      <form @submit="onSubmit">
        <TextInput v-model="name" :errors="nameErrors">
          <template #label>Naam</template>
        </TextInput>

        <ColorInput v-model="color" :errors="colorErrors">
          <template #label>Kleur</template>
        </ColorInput>

        <div class="form-actions">
          <div class="left">
            <div v-if="mutationLoading === true">Submitting...</div>
            <div v-if="mutationHasError === true">
              <ApiValidationErrors
                :api-response="mutationError.response.data"
              />
            </div>
          </div>

          <div class="right">
            <button type="submit" class="btn-blue btn-icon">
              <i :class="actionIcon"></i>
              {{ actionLabel }}
            </button>
          </div>
        </div>
      </form>
    </template>
  </EditModal>
</template>

<script setup>
import { computed } from 'vue';
import CategoriesService from '@/services/resto-api/categories.service';
import { QUERY_KEYS } from '@/utilities/constants';
import { useField, useForm } from 'vee-validate';
import { HEX_COLOR_REGEX } from '@/utilities/validation';
import * as yup from 'yup';
import EditModal from '@/components/manage/common/EditModal.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import TextInput from '@/components/common/form/TextInput.vue';
import ColorInput from '@/components/common/form/ColorInput.vue';
import ApiValidationErrors from '@/components/common/ApiValidationErrors.vue';

const emit = defineEmits(['close']);
const props = defineProps({
  category: {
    type: Object,
    default: null,
  },
});
const isEdit = computed(() => props.category != null);

//#region UI
const actionLabel = computed(() => (isEdit.value ? 'opslaan' : 'aanmaken'));
const actionIcon = computed(() => 'icon-' + (isEdit.value ? 'save' : 'plus'));
//#endregion

//#region query

const createOrUpdateCategory = (formValues) => {
  if (isEdit.value) {
    const request = {
      ...formValues,
      id: props.category.id,
      lastModifiedOn: props.category.lastModifiedOn,
    };
    return CategoriesService.update(request.id, request);
  } else {
    return CategoriesService.create(formValues);
  }
};

const queryClient = useQueryClient();
const {
  isLoading: mutationLoading,
  isError: mutationHasError,
  error: mutationError,
  // isSuccess: mutationSuccess,
  mutate: triggerMutation,
} = useMutation({
  mutationFn: createOrUpdateCategory,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.CATEGORIES });
    tryClose(true);
  },
});
//#endregion

//#region form
const validationSchema = yup.object({
  name: yup.string().required('Naam is verplicht'),
  color: yup
    .string()
    .required('Kleur is verplicht')
    .matches(HEX_COLOR_REGEX, 'Ongeldige kleurcode'),
});
const { handleSubmit, meta: formMeta } = useForm({ validationSchema });

const { value: name, errors: nameErrors } = useField('name', undefined, {
  initialValue: props.category?.name,
});
const { value: color, errors: colorErrors } = useField('color', undefined, {
  initialValue: props.category?.color,
});
const onSubmit = handleSubmit((values) => {
  triggerMutation(values);
});
//#endregion

const tryClose = (force = false) => {
  let close = true;
  if (!force && formMeta.value.dirty)
    close = confirm(
      'There may be unsaved changes, are you sure you want to stop editing?',
    );
  if (close) emit('close');
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.form-actions {
  margin-top: $box-padding;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
</style>
