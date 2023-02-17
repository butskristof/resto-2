import { defineStore } from 'pinia';
import { computed, ref } from 'vue';
import { useProductsQuery } from '@/composables/queries';
import { ORDER_DISCOUNT } from '@/utilities/order-discount';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import OrdersService from '@/services/resto-api/orders.service';
import { QUERY_KEYS } from '@/utilities/constants';

export const useCurrentOrderStore = defineStore('current-order', () => {
  const currentOrder = ref([]);
  const discount = ref(ORDER_DISCOUNT.None);
  const cashReceived = ref(0);
  const cashToReturn = computed(() =>
    Math.max(0, -1 * (total.value - cashReceived.value)),
  );

  const { products } = useProductsQuery(true);

  const extendedCurrentOrder = computed(() => {
    return currentOrder.value.map((orderLine) => {
      const product = products.value.find((p) => p.id === orderLine.productId);
      const toppings = product.toppings.filter((t) =>
        orderLine.toppingIds.includes(t.id),
      );

      const unitPrice =
        product.price +
        toppings.reduce(
          (runningTotal, topping) => runningTotal + topping.price,
          0,
        );

      const totalPrice = orderLine.count * unitPrice;
      return {
        ...orderLine,
        product,
        toppings,
        unitPrice,
        totalPrice,
      };
    });
  });

  const total = computed(() => {
    if (discount.value.value !== ORDER_DISCOUNT.None.value) return 0;
    return extendedCurrentOrder.value.reduce(
      (runningTotal, orderLine) => runningTotal + orderLine.totalPrice,
      0,
    );
  });

  function add({ productId, toppingIds }) {
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

  function reset() {
    currentOrder.value = [];
    discount.value = ORDER_DISCOUNT.None;
    cashReceived.value = 0;
  }

  const queryClient = useQueryClient();
  const { mutate: createOrder } = useMutation({
    mutationFn: () => {
      const request = {
        orderLines: currentOrder.value.map((ol) => ({
          productId: ol.productId,
          toppingIds: [...ol.toppingIds],
          quantity: ol.count,
        })),
        discount: discount.value.value,
      };
      return OrdersService.create(request);
    },
    onSuccess: () => {
      reset();
      return queryClient.invalidateQueries({ queryKey: QUERY_KEYS.ORDERS });
      // TODO tippy
    },
  });

  return {
    orderLines: extendedCurrentOrder,
    total,
    discount,
    cashReceived,
    cashToReturn,

    add,
    increment,
    decrement,
    reset,
    create: createOrder,
  };
});
