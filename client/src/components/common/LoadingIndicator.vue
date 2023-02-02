<template>
  <div class="loader-wrapper">
    <div class="loader"></div>
    <div>
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LoadingIndicator',
};
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

$foreground: $body-text-color;
$background: $white;

.loader-wrapper {
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: 0.5rem;
}

.loader {
  width: 1.25rem; /* the size */
  padding: 2px; /* the border thickness */
  background: $body-text-color; /* the color */

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

//.loader {
//  display: inline-block;
//  border: 2px solid $gray-light; /* Light grey */
//  border-top: 2px solid $body-text-color; /* Blue */
//  border-radius: 50%;
//  width: 1.25rem;
//  height: 1.25rem;
//  //width: 120px;
//  //height: 120px;
//  animation: spin 2s linear infinite;
//}
//
//@keyframes spin {
//  0% {
//    transform: rotate(0deg);
//  }
//  100% {
//    transform: rotate(360deg);
//  }
//}
</style>
