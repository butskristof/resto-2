<template>
  <div class="ticket-quantity-container">
    {{ quantity }}
    <div class="actions">
      <button type="button" class="btn-add" @click="++quantity">+</button>
      <button type="button" class="btn-subtract" @click="--quantity">-</button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'TicketQuantity',
  props: {
    // quantity: {
    //   type: Number,
    //   default: 0,
    // },
  },
  data() {
    return {
      quantity: 10,
    };
  },
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_buttons.scss';

.ticket-quantity-container {
  min-width: 5rem;
}

.actions {
  display: inline-block;
  line-height: 1rem;

  button {
    @include reset-button;
    border-radius: $button-border-radius;
    color: $white;
    padding: 0 0.5rem;
    font-weight: bold;
    height: 1.25rem;

    &:not(:last-of-type) {
      margin-right: 0.25rem;
    }
  }

  .btn-add {
    @include button-green;
  }

  .btn-subtract {
    @include button-red;
  }
}
</style>
