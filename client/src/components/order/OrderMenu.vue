<template>
  <div class="order-menu">
    <h2>Menu</h2>

    <div class="menu-items">
      <div class="menu-search">
        <Search class="search-icon" />
        <input type="text" placeholder="Zoek producten" />
      </div>
      <MenuItem v-for="i in [...Array(100).keys()]" :key="i" />
    </div>
  </div>
</template>

<script>
import MenuItem from '@/components/order/MenuItem.vue';
import { Search } from 'lucide-vue-next';

export default {
  name: 'OrderMenu',
  components: { MenuItem, Search },
};
</script>

<style scoped lang="scss">
@import '@/styles/_mixins.scss';
@import '@/styles/_forms.scss';

.order-menu {
  display: flex;
  flex-direction: column;
}

h2 {
  @include styled-h2;
}

.menu-search {
  margin: auto 1rem 1rem 1rem;
  position: relative;

  .search-icon {
    position: absolute;
    top: 0.5rem;
    left: 0.5rem;
    color: $primary-blue;
  }

  input {
    @include styled-input-base;
    display: block;
    width: 100%;
    padding-left: 2rem;
  }
}

.menu-items {
  overflow: auto;
}
</style>
