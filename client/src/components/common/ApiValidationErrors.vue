<template>
  <div class="api-validation-errors">
    <div class="error" v-for="(errorCodes, property) in errors" :key="property">
      <pre>{{ property }}: {{ errorCodes.join(',') }}</pre>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ApiValidationErrors',
  props: {
    apiResponse: {
      type: Object,
      required: true,
    },
  },
  computed: {
    errors() {
      return this.apiResponse.validationErrors;
    },
  },
};
</script>

<style scoped></style>
