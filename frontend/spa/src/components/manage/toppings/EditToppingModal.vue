<template>
  <EditModal entity="topping" :is-edit="isEdit" @close="tryClose">
    <template #body>
      <form @submit="onSubmit">
        <TextFormInput v-model="name" :errors="nameErrors">
          <template #label>Naam</template>
        </TextFormInput>

        <CurrencyFormInput v-model="price" :errors="priceErrors">
          <template #label>Prijs</template>
        </CurrencyFormInput>

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
import TextFormInput from '@/components/common/form/rows/TextFormInput.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import ToppingsService from '@/services/resto-api/toppings.service';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import { capitalize } from '@/utilities/filters';
import CurrencyFormInput from '@/components/common/form/rows/CurrencyFormInput.vue';
import ApiValidationErrors from '@/components/common/ApiValidationErrors.vue';
import { useToast } from 'vue-toastification';

const toast = useToast();

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
  price: yup
    .number()
    .required('Prijs is verplicht')
    .min(0, 'Prijs kan niet negatief zijn'),
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
    toast.success('Topping ' + (isEdit.value ? 'bijgewerkt' : 'aangemaakt'));
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
@import '@/styles/utilities/_padding-margin.scss';
@import '@/styles/ui/_layout.scss';

.form-actions {
  margin-top: $box-padding;
  @include flex-row-space-between;
}
</style>
