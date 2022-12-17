<template>
  <div class="ticket-container">
    <table class="ticket">
      <thead>
        <tr>
          <th class="product">Product</th>
          <th class="number">
            <tippy content="Aantal"> # </tippy>
          </th>
          <th class="number">
            <tippy content="Eenheidsprijs"> EHP </tippy>
          </th>
          <th class="number">
            <tippy content="Subtotaal"> TOT </tippy>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td class="product">item</td>
          <td class="number">1</td>
          <td class="number">9,00</td>
          <td class="number">9,00</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: 'CurrentOrderTicket',
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.ticket-container {
  flex-grow: 1;
  margin-bottom: 1rem;

  border: 1px solid $secondary-grey;

  //border-radius: $border-radius;
}

.ticket {
  width: 100%;
  font-family: monospace;

  td,
  th {
    padding-right: 1rem;
  }

  thead {
    font-weight: bold;
    font-size: 120%;
    text-align: left;
    //border: 1rem solid $text-color; // TODO
  }

  .product {
    width: 100%;
  }

  .number {
    text-align: right;
  }
}
</style>
