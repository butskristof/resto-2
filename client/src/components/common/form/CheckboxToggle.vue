<template>
  <label class="switch">
    <input type="checkbox" v-model="model" />
    <span class="slider round"></span>
  </label>
</template>

<script setup>
import { computed } from 'vue';

const emit = defineEmits(['update:modelValue']);
const props = defineProps({
  modelValue: {
    type: Boolean,
    default: false,
  },
});

const model = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value),
});
</script>

<style scoped lang="scss">
@import '@/styles/_variables.scss';

$background: $gray-light;
$foreground: $klj-blue;

//$width: 60px;
//$height: 34px;
//$slider-size: 26px;
$width: 40px;
$height: 23px;
$slider-size: 17px;
$slider-margin: calc(($height - $slider-size) / 2);

// https://www.w3schools.com/howto/howto_css_switch.asp
/* The switch - the box around the slider */
.switch {
  position: relative;
  display: inline-block;
  width: $width;
  height: $height;
}

/* Hide default HTML checkbox */
.switch input {
  opacity: 0;
  width: 0;
  height: 0;
}

/* The slider */
.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: $background;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: '';
  height: $slider-size;
  width: $slider-size;
  left: $slider-margin;
  bottom: $slider-margin;
  background-color: $white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

input:checked + .slider {
  background-color: $foreground;
}

input:focus + .slider {
  box-shadow: 0 0 1px $foreground;
}

input:checked + .slider:before {
  -webkit-transform: translateX($slider-size);
  -ms-transform: translateX($slider-size);
  transform: translateX($slider-size);
}

/* Rounded sliders */
.slider.round {
  border-radius: $height;
}

.slider.round:before {
  border-radius: 50%;
}
</style>
