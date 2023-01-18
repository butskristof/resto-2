<template>
  <h2>Manage</h2>
  <RouterView />
</template>

<script>
import { RouterView } from 'vue-router';

export default {
  name: 'ManagePage',
  components: [RouterView],
};
</script>

<style scoped></style>
