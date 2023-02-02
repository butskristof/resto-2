<template>
  <div>
    <h2>Create category</h2>
    <form @submit="onSubmit">
      <TextInput v-model="name" :errors="nameErrors">
        <template #label>Name</template>
      </TextInput>

      <ColorInput v-model="color" :errors="colorErrors">
        <template #label>Colour</template>
      </ColorInput>

      <div class="form-actions">
        <div class="left">
          <pre>{{ JSON.stringify(formErrors, null, 2) }}</pre>
        </div>
        <div class="right">
          <button type="submit">Create category</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script setup>
import * as yup from 'yup';
import { useField, useForm } from 'vee-validate';
import TextInput from '@/components/common/form/TextInput.vue';
import ColorInput from '@/components/common/form/ColorInput.vue';
import { HEX_COLOR_REGEX } from '@/utilities/validation';

const validationSchema = yup.object({
  name: yup.string().trim().required(),
  color: yup
    .string()
    .trim()
    .required()
    .matches(HEX_COLOR_REGEX, 'Ongeldige kleurcode'),
});

const { errors: formErrors, handleSubmit } = useForm({ validationSchema });
const { value: name, errors: nameErrors } = useField('name');
const { value: color, errors: colorErrors } = useField('color');

const onSubmit = handleSubmit((values) => {
  console.log(values);
});
</script>

<style scoped lang="scss">
.form-actions {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-start;
  padding: 1rem;
}
</style>
