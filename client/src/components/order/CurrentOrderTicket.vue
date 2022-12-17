<template>
  <div class="ticket-container">
    <table class="ticket">
      <thead>
        <tr>
          <th class="product">Product</th>
          <th class="number quantity">
            <tippy content="Aantal"> # </tippy>
          </th>
          <th class="number price">
            <tippy content="Eenheidsprijs"> EHP </tippy>
          </th>
          <th class="number price">
            <tippy content="Subtotaal"> TOT </tippy>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="i in [...Array(100).keys()]" :key="i">
          <td class="product">item</td>
          <td class="number">
            <TicketQuantity />
          </td>
          <td class="number price">9,00</td>
          <td class="number price">9,00</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import TicketQuantity from '@/components/order/TicketQuantity.vue';

export default {
  name: 'CurrentOrderTicket',
  components: { TicketQuantity },
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

.ticket-container {
  flex-grow: 1;
  margin-bottom: 1rem;

  border: 2px solid black;
  overflow: auto;
}

.ticket {
  width: 100%;
  font-family: monospace;
  border-collapse: collapse;

  td,
  th {
    padding-right: 0.5rem;
    padding-left: 0.5rem;
  }

  tbody {
    tr {
      line-height: 1.75rem;

      &:nth-child(even) {
        background-color: #eee;
      }
    }
  }

  thead {
    font-weight: bold;
    font-size: 120%;
    text-align: left;
    border-bottom: 2px dashed black;

    tr {
      line-height: 1.5rem;
    }
  }

  .product {
    width: 100%;
  }

  .number {
    text-align: right;
  }

  .quantity {
    min-width: 4rem;
  }

  .price {
    min-width: 5rem;
  }
}
</style>
