<template>
  <div class="current-order">
    <h2>Order</h2>
    <CurrentOrderTicket />
    <div class="discounts">
      <div>Bestelling voor</div>
      <div>Klant / Helper / Leiding</div>
    </div>
    <CurrentOrderCalculator />
    <div class="actions">
      <RestoButton theme="red">Wissen</RestoButton>
      <RestoButton theme="green">Bestellen</RestoButton>
    </div>
  </div>
</template>

<script>
import RestoButton from '@/components/common/RestoButton.vue';
import CurrentOrderCalculator from '@/components/order/CurrentOrderCalculator.vue';
import CurrentOrderTicket from '@/components/order/CurrentOrderTicket.vue';

export default {
  name: 'CurrentOrder',
  components: { CurrentOrderTicket, CurrentOrderCalculator, RestoButton },
};
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';

.current-order {
  display: flex;
  flex-direction: column;
}

h2 {
  @include styled-h2;
}

.spacer {
  flex-grow: 1;
}

.actions {
  margin-top: 1rem;

  display: flex;
  flex-direction: row;
  justify-content: space-between;

  button {
    font-size: 125%;
  }
}
</style>
