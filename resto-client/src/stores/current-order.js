import { defineStore } from 'pinia';
import { computed, ref } from 'vue';
import { useProductsStore } from '@/stores/products';

export const useCurrentOrderStore = defineStore('current-order', () => {
  const currentOrder = ref([]);

  const extendedCurrentOrder = computed(() => {
    const { products } = useProductsStore();
    return currentOrder.value.map((orderLine) => {
      const product = products.find((p) => p.id === orderLine.productId);
      const toppings = product.toppings.filter((t) =>
        orderLine.toppingIds.includes(t.id),
      );

      let description = product.name;
      if (toppings.length > 0) {
        description += `, ${toppings.map((t) => t.name).join(', ')}`;
      }

      const unitPrice =
        product.price +
        toppings.reduce(
          (runningTotal, topping) => runningTotal + topping.price,
          0,
        );

      const totalPrice = orderLine.count * unitPrice;
      return {
        description,
        count: orderLine.count,
        unitPrice,
        totalPrice,
      };
    });
  });
  const currentOrderTotal = computed(() => {
    return extendedCurrentOrder.value.reduce(
      (runningTotal, orderLine) => runningTotal + orderLine.totalPrice,
      0,
    );
  });

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

  function increment(index) {
    currentOrder.value[index].count += 1;
  }
  function decrement(index) {
    const item = currentOrder.value[index];
    item.count -= 1;
    if (item.count === 0) currentOrder.value.splice(index, 1);
  }

  return {
    currentOrder,
    extendedCurrentOrder,
    currentOrderTotal,

    addToOrder,
    increment,
    decrement,
  };
});
