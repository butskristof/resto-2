<template>
  <div class="topping-picker">
    <VueMultiselect
      v-model="model"
      :loading="toppingsLoading"
      :options="toppings"
      :disabled="disabled"
      track-by="id"
      label="name"
      :multiple="multiple"
      :searchable="true"
      :close-on-select="false"
      :show-labels="false"
      :placeholder="placeholder"
    />
    <div v-if="toppingQueryError" class="error">
      Toppings konden niet worden ingeladen, probeer het later opnieuw.
    </div>
  </div>
</template>

<script setup>
import VueMultiselect from 'vue-multiselect';
import { computed } from 'vue';
import { useToppingsQuery } from '@/composables/queries';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  modelValue: {
    type: Object,
    default: null,
  },
  multiple: {
    type: Boolean,
    default: true,
  },
});
// TODO VueUse composable
const model = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value),
});

const placeholder = computed(() => {
  let placeholder = 'Kies topping';
  if (props.multiple) placeholder += 's';
  return placeholder;
});

const {
  toppings,
  isLoading: toppingsLoading,
  isError: toppingQueryError,
} = useToppingsQuery(true);
const disabled = computed(
  () => toppingsLoading.value || toppingQueryError.value,
);
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.error {
  @include error-text;
  margin-top: 0.5rem;
}
</style>
