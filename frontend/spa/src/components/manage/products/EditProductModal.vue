<template>
  <EditModal entity="gerecht" :is-edit="isEdit" @close="tryClose">
    <template #body>
      <form @submit="onSubmit">
        <TextFormInput v-model="name" :errors="nameErrors">
          <template #label>Naam</template>
        </TextFormInput>

        <CurrencyFormInput v-model="price" :errors="priceErrors">
          <template #label>Prijs</template>
        </CurrencyFormInput>

        <CheckboxFormInput
          v-model="multipleToppingsAllowed"
          :errors="multipleToppingsAllowedErrors"
        >
          <template #label>Meerdere toppings</template>
        </CheckboxFormInput>

        <GenericFormInput :errors="categoryErrors">
          <template #label>Categorie</template>
          <template #input>
            <CategoryPicker v-model="category" />
          </template>
        </GenericFormInput>

        <GenericFormInput :errors="toppingErrors">
          <template #label>Toppings</template>
          <template #input>
            <ToppingPicker v-model="toppings" />
          </template>
        </GenericFormInput>

        <div class="form-actions">
          <div class="left">
            <LoadingIndicator v-if="mutationLoading">
              Product {{ actionLabel }}
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
import { computed, ref, watch } from 'vue';
import CategoryPicker from '@/components/manage/products/edit/CategoryPicker.vue';
import GenericFormInput from '@/components/common/form/rows/GenericFormInput.vue';
import * as yup from 'yup';
import { useField, useForm } from 'vee-validate';
import { capitalize } from '@/utilities/filters';
import TextFormInput from '@/components/common/form/rows/TextFormInput.vue';
import CurrencyFormInput from '@/components/common/form/rows/CurrencyFormInput.vue';
import CheckboxFormInput from '@/components/common/form/rows/CheckboxFormInput.vue';
import ToppingPicker from '@/components/manage/products/edit/ToppingPicker.vue';
import ProductsService from '@/services/resto-api/products.service';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';
import LoadingIndicator from '@/components/common/LoadingIndicator.vue';
import ApiValidationErrors from '@/components/common/ApiValidationErrors.vue';
import { useToast } from 'vue-toastification';

const toast = useToast();

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
const validationSchema = yup.object({
  name: yup.string().required('Naam is verplicht'),
  price: yup
    .number()
    .nullable()
    .required('Prijs is verplicht')
    .min(0, 'Prijs kan niet negatief zijn'),
  multipleToppingsAllowed: yup.boolean(),
  categoryId: yup.string().required('Categorie is verplicht'),
  toppingIds: yup.array().of(yup.string()),
});
const { handleSubmit, meta: formMeta } = useForm({ validationSchema });
const { value: name, errors: nameErrors } = useField('name', undefined, {
  initialValue: props.product?.name,
});
const { value: price, errors: priceErrors } = useField('price', undefined, {
  initialValue: props.product?.price,
});
const {
  value: multipleToppingsAllowed,
  errors: multipleToppingsAllowedErrors,
} = useField('multipleToppingsAllowed', undefined, {
  initialValue: props.product?.multipleToppingsAllowed ?? false,
});

const category = ref(props.product?.category ?? null);
const { value: categoryId, errors: categoryErrors } = useField(
  'categoryId',
  undefined,
  {
    initialValue: props.product?.category.id ?? null,
  },
);
watch(category, () => (categoryId.value = category.value?.id), {
  immediate: props.product != null,
});

const toppings = ref(props.product?.toppings ?? []);
const { value: toppingIds, errors: toppingErrors } = useField(
  'toppingIds',
  undefined,
  {
    initialValue: props.product?.toppings.map((t) => t.id) ?? [],
  },
);
watch(toppings, () => (toppingIds.value = toppings.value.map((t) => t.id)), {
  immediate: props.product != null,
});

const onSubmit = handleSubmit((values) => {
  triggerMutation(values);
});
//#endregion

//#region query
const createOrUpdateProduct = (formValues) => {
  if (isEdit.value) {
    const request = {
      ...formValues,
      id: props.product.id,
      lastModifiedOn: props.product.lastModifiedOn,
    };
    return ProductsService.update(request.id, request);
  } else {
    return ProductsService.create(formValues);
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
  mutationFn: createOrUpdateProduct,
  onSuccess: () => {
    toast.success('Gerecht ' + (isEdit.value ? 'bijgewerkt' : 'aangemaakt'));
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.PRODUCTS });
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
