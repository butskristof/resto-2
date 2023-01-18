import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useCurrentOrderStore = defineStore('current-order', () => {
  const currentOrder = ref([]);

  function addToOrder({ productId, toppingIds }) {
    const existing = currentOrder.value.find(
      (ol) =>
        ol.productId === productId &&
        ol.toppingIds.length === toppingIds.length &&
        ol.toppingIds.every((oltid) => toppingIds.includes(oltid)),
    );
    if (existing == null) {
      currentOrder.value.push({
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
