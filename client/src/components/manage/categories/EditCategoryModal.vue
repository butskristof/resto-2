<template>
  <BaseModal>
    <template #header>
      <div class="header">
        <div class="left">
          <h3>{{ actionLabel }}</h3>
        </div>
        <div class="right">
          <button type="button" @click="tryClose">x</button>
        </div>
      </div>
    </template>

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
            <button type="submit">{{ actionLabel }}</button>
          </div>
        </div>
      </form>
    </template>
  </BaseModal>
</template>

<script setup>
import BaseModal from '@/components/common/BaseModal.vue';
import TextInput from '@/components/common/form/TextInput.vue';
import ColorInput from '@/components/common/form/ColorInput.vue';
import { useField, useForm } from 'vee-validate';
import * as yup from 'yup';
import { HEX_COLOR_REGEX } from '@/utilities/validation';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import CategoriesService from '@/services/resto-api/categories.service';
import ApiValidationErrors from '@/components/common/ApiValidationErrors.vue';
import { computed } from 'vue';

const emit = defineEmits(['close']);
const props = defineProps({
  category: {
    type: Object,
    default: null,
  },
});
const isEdit = computed(() => props.category != null);

const queryClient = useQueryClient();

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

const {
  isLoading: mutationLoading,
  isError: mutationHasError,
  error: mutationError,
  isSuccess: mutationSuccess,
  mutate,
} = useMutation({
  mutationFn: createOrUpdateCategory,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.CATEGORIES });
    tryClose(true);
  },
});

//#region form validation
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

const onSubmit = handleSubmit(mutate);
//#endregion

const tryClose = (force = false) => {
  let close = true;
  if (!force && formMeta.value.dirty)
    close = confirm(
      'There may be unsaved changes, are you sure you want to stop editing?',
    );
  if (close) emit('close');
};

const actionLabel = computed(
  () => 'Categorie ' + (isEdit.value ? 'bewerken' : 'aanmaken'),
);
</script>

<style scoped lang="scss">
.header,
.form-actions {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.form-actions {
  margin-top: 1rem;
}
</style>
