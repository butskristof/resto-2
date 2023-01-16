import { defineStore } from 'pinia';
import { reactive } from 'vue';

export const useCurrentOrderStore = defineStore('current-order', () => {
  const currentOrder = reactive([]);

  function addToOrder({ productId, toppingIds }) {
    const existing = currentOrder.find(
      (ol) =>
        ol.productId === productId &&
        ol.toppingIds.length === toppingIds.length &&
        ol.toppingIds.every((oltid) => toppingIds.includes(oltid)),
    );
    if (existing == null) {
      currentOrder.push({
        productId,
        toppingIds,
        count: 1,
      });
    } else {
      existing.count += 1;
    }
  }

  return { currentOrder, addToOrder };
});
