<template>
  <footer>
    <div class="left">
      <router-link
        v-if="showOrderLink"
        v-tippy="getTippyConfiguration('Bestellen')"
        :to="{ name: routeInfo.order.name }"
      >
        <i class="icon-soup"></i>
      </router-link>
    </div>
    <div class="right">
      <div>
        <a
          v-tippy="getTippyConfiguration('GitHub')"
          href="https://github.com/butskristof/resto-2"
          target="_blank"
        >
          <i class="icon-github"></i>
        </a>
      </div>

      <div>
        <router-link
          v-tippy="getTippyConfiguration('Bestelhistoriek')"
          :to="{ name: routeInfo.orderHistory.name }"
        >
          <i class="icon-history"></i>
        </router-link>
      </div>

      <div>
        <router-link
          v-tippy="getTippyConfiguration('Statistieken')"
          :to="{ name: routeInfo.stats.name }"
        >
          <i class="icon-line-chart"></i>
        </router-link>
      </div>

      <div>
        <router-link
          v-tippy="getTippyConfiguration('Instellingen')"
          :to="{ name: routeInfo.manage.name }"
        >
          <i class="icon-settings"></i>
        </router-link>
      </div>
    </div>
  </footer>
</template>

<script setup>
import routeInfo from '@/router/route-info';
import { useRoute } from 'vue-router';
import { computed } from 'vue';
function getTippyConfiguration(content) {
  return { content, placement: 'top-end' };
}
const currentRoute = useRoute();
const showOrderLink = computed(
  () => currentRoute.name !== routeInfo.order.name,
);
</script>

<style scoped lang="scss">
@use '@/styles/ui/_layout.scss';
@use '@/styles/utilities/_padding-margin.scss';
@use '@/styles/utilities/_typography.scss';

footer {
  padding: calc(#{padding-margin.$box-padding} / 2) padding-margin.$box-padding;
  @include layout.flex-row-space-between;

  a {
    @include typography.reset-link;
  }
  @include typography.contrast-text;
  @include typography.contrast-link;
}

.right {
  @include layout.flex-row;
  gap: 0.5rem;
}
</style>
