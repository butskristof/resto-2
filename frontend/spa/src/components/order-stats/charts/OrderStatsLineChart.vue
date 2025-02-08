<template>
  <div class="order-stats">
    <h3>Verloop bestellingen over tijd</h3>
    <Line :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup>
import { useOrderStatisticsQuery } from '@/composables/queries';
import { computed } from 'vue';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  TimeScale,
} from 'chart.js';
import { Line } from 'vue-chartjs';
import 'chartjs-adapter-moment';

ChartJS.register(
  CategoryScale,
  LinearScale,
  TimeScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
);

const { data } = useOrderStatisticsQuery();
const stats = computed(() => data.value?.orderStatistics);

const chartData = computed(() => {
  const data = {
    labels: [],
    datasets: [
      {
        data: [],
        cubicInterpolationMode: 'monotone',
        label: 'Bestellingen',
        borderColor: '#ed2920',
      },
    ],
  };
  if (stats.value != null) {
    data.labels = stats.value.map((o) => new Date(o.timestamp));
    data.datasets[0].data = stats.value.map((o) => o.count);
  }
  return data;
});
const chartOptions = {
  responsive: true,
  scales: {
    x: {
      type: 'time',
      time: {
        unit: 'hour',
        displayFormats: {
          hour: 'yyyy-MM-DD hh:mm',
        },
      },
      title: {
        display: true,
        text: 'Tijdstip',
      },
    },
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

<style scoped lang="scss"></style>
