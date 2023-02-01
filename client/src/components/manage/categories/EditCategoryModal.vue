<template>
  <BaseModal>
    <template #header>
      <div class="header">
        <div class="left">
          <h3>Categorie bewerken</h3>
        </div>
        <div class="right">
          <button type="button" @click="close">x</button>
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
          <div class="left"></div>
          <div class="right">
            <button type="submit">Categorie aanmaken</button>
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

const emit = defineEmits(['close']);

const validationSchema = yup.object({
  name: yup.string().required('Naam is verplicht'),
  color: yup
    .string()
    .required('Kleur is verplicht')
    .matches(HEX_COLOR_REGEX, 'Ongeldige kleurcode'),
});
const { handleSubmit } = useForm({ validationSchema });
const { value: name, errors: nameErrors } = useField('name');
const { value: color, errors: colorErrors } = useField('color');

const onSubmit = handleSubmit((values) => {
  console.log(values);
  close();
});

const close = () => {
  // TODO check unsaved changed
  emit('close');
};
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
