<template>
  <EditModal entity="gerecht" @close="tryClose" :is-edit="isEdit">
    <template #body>
      <form @submit="onSubmit">
        <TextInput v-model="name" :errors="nameErrors">
          <template #label>Naam</template>
        </TextInput>

        <CurrencyInput v-model="price" :errors="priceErrors">
          <template #label>Prijs</template>
        </CurrencyInput>

        <CheckBoxInput
          v-model="multipleToppingsAllowed"
          :errors="multipleToppingsAllowedErrors"
        >
          <template #label>Meerdere toppings</template>
        </CheckBoxInput>

        <GenericInput :errors="categoryErrors">
          <template #label>Categorie</template>
          <template #input>
            <CategoryPicker v-model="category" />
          </template>
        </GenericInput>

        <GenericInput :errors="toppingErrors">
          <template #label>Toppings</template>
          <template #input>
            <ToppingPicker v-model="toppings" />
          </template>
        </GenericInput>

        <div class="form-actions">
          <div class="left"></div>

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
import GenericInput from '@/components/common/form/GenericInput.vue';
import * as yup from 'yup';
import { useField, useForm } from 'vee-validate';
import { capitalize } from '@/utilities/filters';
import TextInput from '@/components/common/form/TextInput.vue';
import CurrencyInput from '@/components/common/form/CurrencyInput.vue';
import CheckBoxInput from '@/components/common/form/CheckBoxInput.vue';
import ToppingPicker from '@/components/manage/products/edit/ToppingPicker.vue';
import ProductsService from '@/services/resto-api/products.service';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { QUERY_KEYS } from '@/utilities/constants';

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
    .required('Prijs is verplicht')
    .min(0, 'Prijs kan niet negatief zijn'),
  multipleToppingsAllowed: yup.boolean(),
  categoryId: yup.string().required('Categorie is verplicht'),
  toppingIds: yup.array().of(yup.string()),
});
const { handleSubmit, meta: formMeta } = useForm({ validationSchema });
const { value: name, errors: nameErrors } = useField('name');
const { value: price, errors: priceErrors } = useField('price');
const {
  value: multipleToppingsAllowed,
  errors: multipleToppingsAllowedErrors,
} = useField('multipleToppingsAllowed', undefined, {
  initialValue: false,
});

const category = ref(null);
const { value: categoryId, errors: categoryErrors } = useField(
  'categoryId',
  undefined,
  {
    initialValue: null,
  },
);
watch(category, () => (categoryId.value = category.value?.id));

const toppings = ref([]);
const { value: toppingIds, errors: toppingErrors } = useField(
  'toppingIds',
  undefined,
  {
    initialValue: [],
  },
);
watch(toppings, () => (toppingIds.value = toppings.value.map((t) => t.id)));

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
  mutate: triggerMutation,
} = useMutation({
  mutationFn: createOrUpdateProduct,
  onSuccess: () => {
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
@import '@/styles/_variables.scss';

.form-actions {
  margin-top: $box-padding;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
</style>
