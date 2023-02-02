<template>
  <EditModal entity="topping" @close="tryClose" :is-edit="isEdit">
    <template #body>
      <form @submit="onSubmit">
        <TextInput v-model="name" :errors="nameErrors">
          <template #label>Naam</template>
        </TextInput>

        <NumberInput v-model="price" :errors="priceErrors">
          <template #label>Prijs</template>
        </NumberInput>

        <div class="form-actions">
          <div class="left">
            <LoadingIndicator v-if="mutationLoading">
              Topping {{ actionLabel }}
            </LoadingIndicator>
            <div v-if="mutationHasError">
              <ApiValidationErrors
                :api-response="mutationError.response.data"
              />
            </div>
          </div>

          <div class="right">
            <button type="submit" class="btn-blue btn-icon">
              <i :class="actionIcon"></i>
              {{ capitalize(actionLabel) }}
            </button>
          </div>
        </div>
      </form>
    </template>
  </EditModal>
</template>

<script setup>
import EditModal from '@/components/manage/common/EditModal.vue';
import { computed } from 'vue';
import * as yup from 'yup';
import { useField, useForm } from 'vee-validate';
import TextInput from '@/components/common/form/TextInput.vue';
import NumberInput from '@/components/common/form/NumberInput.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import ToppingsService from '@/services/resto-api/toppings.service';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import { capitalize } from '@/utilities/filters';

const emit = defineEmits(['close']);
const props = defineProps({
  topping: {
    type: Object,
    default: null,
  },
});
const isEdit = computed(() => props.topping != null);

//#region UI
const actionLabel = computed(() => (isEdit.value ? 'opslaan' : 'aanmaken'));
const actionIcon = computed(() => 'icon-' + (isEdit.value ? 'save' : 'plus'));
//#endregion

//#region form
const validationSchema = yup.object({
  name: yup.string().required('Naam is verplicht'),
  price: yup.number().required('Prijs is verplicht'),
});
const { handleSubmit, meta: formMeta } = useForm({ validationSchema });
const { value: name, errors: nameErrors } = useField('name', undefined, {
  initialValue: props.topping?.name,
});
const { value: price, errors: priceErrors } = useField('price', undefined, {
  initialValue: props.topping?.price,
});
const onSubmit = handleSubmit((values) => {
  triggerMutation(values);
});
//#endregion

//#region query
const createOrUpdateTopping = (formValues) => {
  if (isEdit.value) {
    const request = {
      ...formValues,
      id: props.topping.id,
      lastModifiedOn: props.topping.lastModifiedOn,
    };
    return ToppingsService.update(request.id, request);
  } else {
    return ToppingsService.create(formValues);
  }
};

const queryClient = useQueryClient();
const {
  isLoading: mutationLoading,
  isError: mutationHasError,
  error: mutationError,
  mutate: triggerMutation,
} = useMutation({
  mutationFn: createOrUpdateTopping,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.TOPPINGS });
    tryClose(true);
  },
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