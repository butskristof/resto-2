<template>
  <header>
    <h1>Restaurantdag</h1>
  </header>

  <main>
    <RouterView />
  </main>

  <footer>
    <div class="copyright">&copy; KLJ Wiekevorst</div>
    <div class="links">
      <a href="#">
        <Github size="1rem" />
      </a>
      <a href="#">
        <Settings size="1rem" />
      </a>
    </div>
  </footer>
</template>

<script setup>
import { RouterView } from 'vue-router';
import { Github, Settings } from 'lucide-vue-next';
</script>

<style lang="scss">
#app {
  height: 100%;
  max-width: 1280px;
  margin: auto;
  padding: 0 1rem;

  > * {
    margin: auto;
  }
}
</style>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

$header-height: 4rem;
$footer-height: 2rem;

header {
  padding-top: 1rem;
  margin-bottom: 1rem;
  height: $header-height;
  text-align: center;

  h1 {
    color: $meta-text-color;
    @include brand-font;

    margin: 0;
  }
}

main {
  height: calc(100vh - $header-height - $footer-height);
  width: 100%;
  min-width: 860px;

  border-radius: 0.75rem;
  padding: 1rem;
  background-color: $white;
}

footer {
  height: $footer-height;
  font-size: 80%;

  color: $meta-text-color;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-end;
  padding-bottom: 0.25rem;

  .links {
    a {
      color: $white;
    }

    :not(:last-child) {
      margin-right: 0.5rem;
    }
  }
}
</style>
