<template>
  <footer>
    <div class="left">&copy;</div>
    <div class="right">
      <div>github</div>
      <div>settings</div>
    </div>
  </footer>
</template>

<script>
export default {
  name: 'AppFooter',
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

footer {
  color: $meta-text-color;
  font-size: 80%;
  padding: 0.5rem 1rem;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.right {
  display: flex;
  flex-direction: row;
  gap: 0.5rem;
}
</style>
