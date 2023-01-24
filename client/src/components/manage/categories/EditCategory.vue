<template>
  <BaseModal>
    <template #header>
      <div class="header">
        <div class="title">Edit category</div>
        <div class="actions">
          <button type="button" @click="tryClose">x</button>
        </div>
      </div>
    </template>
    <template #body>
      <form @submit.prevent="submit">
        <div class="form-group">
          <label for="name">Name</label>
          <input type="text" id="name" v-model.trim="name" />
        </div>
        <div class="form-footer">
          <div class="validation-errors">validation errors go here</div>
          <div class="actions">
            <button type="submit">Submit</button>
          </div>
        </div>
      </form>
    </template>
  </BaseModal>
</template>

<script setup>
import { computed, onMounted, ref } from 'vue';
import BaseModal from '@/components/common/BaseModal.vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import CategoriesService from '@/services/resto-api/categories.service';
import { QUERY_KEYS } from '@/utilities/constants';

const emit = defineEmits(['close']);
const props = defineProps({
  show: {
    type: Boolean,
    default: false,
  },
  category: {
    type: Object,
    default: null,
  },
});
const isEdit = computed(() => props.category != null);

const name = ref('');
const request = computed(() => ({
  name: name.value,
}));

function submit() {
  mutation.mutate();
}

const queryClient = useQueryClient();
const mutation = useMutation({
  mutationFn: () => {
    if (isEdit.value) {
      const r = {
        ...request.value,
        id: props.category.id,
        lastModifiedOn: props.category.lastModifiedOn,
      };
      CategoriesService.update(r.id, r);
    } else {
      CategoriesService.create(request.value);
    }
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: QUERY_KEYS.CATEGORIES });
    close();
  },
});

function tryClose() {
  // TODO verify form dirty & show warning
  close();
}

function close() {
  emit('close');
}

onMounted(() => {
  if (props.category != null) {
    name.value = props.category.name;
  }
});
</script>

<style scoped lang="scss">
.header,
.form-footer {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.header {
  margin-bottom: 1rem;
}

.form-footer {
  margin-top: 1rem;
  flex-wrap: wrap;
}

.form-group {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;

  label {
    display: inline-block;
    min-width: 150px;
  }

  input {
    flex-grow: 1;
    min-width: 150px;
  }
}
</style>
