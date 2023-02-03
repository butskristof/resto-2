<template>
  <footer>
    <div class="left"></div>
    <div class="right">
      <div>
        <a href="https://github.com/butskristof/resto-2" target="_blank"
          ><i class="icon-github"></i
        ></a>
      </div>

      <div>
        <a href="#"><i class="icon-line-chart"></i></a>
      </div>

      <div>
        <router-link to="/manage">
          <i class="icon-settings"></i>
        </router-link>
      </div>
    </div>
  </footer>
</template>

<style scoped lang="scss">
@import '@/styles/_variables.scss';
@import '@/styles/_mixins.scss';

footer {
  padding: calc($box-padding / 2) $box-padding;

  display: flex;
  flex-direction: row;
  justify-content: space-between;

  color: $meta-text-color;
  font-size: 80%;
  @include meta-link;
}

.right {
  display: flex;
  flex-direction: row;
  gap: 0.5rem;
}
</style>
