<template>
  <div class="product-stats">
    <h3>Bestellingen per product</h3>
    <Bar :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup>
import { Bar } from 'vue-chartjs';
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  BarElement,
  CategoryScale,
  LinearScale,
} from 'chart.js';

import { useOrderStatisticsQuery } from '@/composables/queries';
import { computed } from 'vue';
const { data } = useOrderStatisticsQuery();
const products = computed(() => data.value?.productStatistics);

ChartJS.register(Title, Tooltip, BarElement, CategoryScale, LinearScale);

const chartData = computed(() => {
  const data = {
    labels: [],
    datasets: [
      {
        label: 'Totaal',
        data: [],
        skipNull: true,
        borderWidth: 1,
      },
    ],
  };
  if (products.value != null) {
    data.labels = products.value.map((p) => p.name);
    data.datasets[0].backgroundColor = products.value.map(
      (p) => p.category.color,
    );
    data.datasets[0].borderColor = new Array(products.value.length).fill(
      '#ccc',
    );

    let i = 0;
    for (let product of products.value) {
      data.datasets[0].data.push(product.quantity);

      for (let topping of product.toppings) {
        let dataset = data.datasets.find((ds) => ds.label === topping.name);
        if (dataset == null) {
          dataset = {
            label: topping.name,
            data: new Array(products.value.length).fill(null),
            backgroundColor: products.value.map((p) => p.category.color),
            borderColor: '#ccc',
            borderWidth: 1,
            skipNull: true,
          };
          data.datasets.push(dataset);
        }
        dataset.data[i] = topping.quantity === 0 ? null : topping.quantity;
      }
      ++i;
    }
  }
  return data;
});
const chartOptions = {
  responsive: true,
  scales: {
    y: {
      title: {
        display: true,
        text: 'Aantal',
      },
      min: 0,
    },
  },
};
</script>

<style scoped></style>
