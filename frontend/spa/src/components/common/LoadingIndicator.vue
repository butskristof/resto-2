<template>
  <div class="loader-wrapper">
    <div class="loader"></div>
    <div>
      <slot></slot>
    </div>
  </div>
</template>

<style scoped lang="scss">
@use '@/styles/_colors.scss';
@use '@/styles/ui/_layout.scss';

$foreground: colors.$body-text-color;
$background: white;

.loader-wrapper {
  @include layout.flex-row;
  align-items: center;
  gap: 0.5rem;
}

.loader {
  width: 1.25rem; /* the size */
  padding: 2px; /* the border thickness */
  background: colors.$body-text-color; /* the color */

  aspect-ratio: 1;
  border-radius: 50%;
  --_m: conic-gradient(#0000, #000), linear-gradient(#000 0 0) content-box;
  -webkit-mask: var(--_m);
  mask: var(--_m);
  -webkit-mask-composite: source-out;
  mask-composite: subtract;
  box-sizing: border-box;
  animation: load 1s linear infinite;
}

@keyframes load {
  to {
    transform: rotate(1turn);
  }
}
</style>
